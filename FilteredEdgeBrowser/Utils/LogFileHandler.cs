using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FilteredEdgeBrowser.Utils
{
    class DataLock<T>
    {
        object myLock = new object();
        T data;

        public DataLock() { }
        public DataLock(T startValue) { data = startValue; }

        public void safeArea(Action<Func<T>, Action<T>> callback)
        {
            lock (myLock)
            {
                callback(() => { return data; }, (T newValue) => { data = newValue; });
            }
        }
    }

    class LogFileHandler
    {
        public const string DataSeperator = "|_&~%~&_|";

        private string oneLine(string input) { return input.Replace("\r", "").Replace("\n", ""); }

        private string myFilePath = "";
        public LogFileHandler(string filepath)
        {
            myFilePath = filepath;
        }

        public Action onSearchFinish;
        public Action<int> onProgressUpdated;

        Thread mainSearchThread = null;
        DataLock<bool> isThreadRunning = new DataLock<bool>(false);

        private bool stopSearchFlagUp = false;
        private string searchText = "";

        public void updateSearchParams(string newSearchText)
        {
            searchText = newSearchText;
            isThreadRunning.safeArea((alive, set) =>
            {
                if (!alive())
                {
                    //Spawn thread
                    mainSearchThread = new Thread(mainSearchLoop);
                    mainSearchThread.Start();
                }
            });
        }

        public void stopSearch()
        {
            isThreadRunning.safeArea((alive, set) =>
            {
                if (alive())
                {
                    stopSearchFlagUp = true;
                }
                // else the thread is stopped!
            });
        }

        private int _progress = 0;
        public int Progress()
        {
            return _progress;
        }

        public string[] UrlResults = new string[100];
        private int currentResultIndex = 0;

        private void mainSearchLoop()
        {
            isThreadRunning.safeArea((get, set) => { set(true); });

            string currentSearch = searchText;
            currentResultIndex = 0;
            if (currentSearch == null || currentSearch.Length > 0)
            {
                stopSearchFlagUp = true;
            }

            bool isThreadStopping = stopSearchFlagUp;
            bool isSearchChanged = (currentSearch == searchText);

            while (!isThreadStopping)
            {
                using (ReverseLineReader file = new ReverseLineReader(myFilePath))
                {
                    foreach (string line in file)
                    {
                        while (!isThreadStopping && !isSearchChanged)
                        {

                            if (line.IndexOf(currentSearch, StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                string[] data = line.Split(new[] { DataSeperator }, StringSplitOptions.RemoveEmptyEntries);
                                if (data.Length == 2)
                                {
                                    UrlResults[currentResultIndex] = data[1];
                                    currentResultIndex++;

                                    if (currentResultIndex == UrlResults.Length)
                                    {
                                        isThreadStopping = true;
                                        onSearchFinish?.Invoke();
                                    }
                                }
                            }

                            int new_progress = (int)
                                (
                                (
                                1f -
                                file.myStream.Position * 1.0f / (file.myStream.Length + 1)
                                ) * 100.0f
                                );
                            if (Math.Abs(new_progress - _progress) > 1)
                            {
                                onProgressUpdated?.Invoke(new_progress);
                            }
                            _progress = new_progress;

                            isThreadStopping = stopSearchFlagUp;
                            isSearchChanged = (currentSearch == searchText);
                        }
                        break;
                    }
                }

                if (!isThreadStopping && isSearchChanged)
                {
                    currentSearch = searchText;
                    currentResultIndex = 0;
                    if (currentSearch == null || currentSearch.Length > 0)
                    {
                        stopSearchFlagUp = true;
                    }
                }
            }

            isThreadRunning.safeArea((get, set) => { set(false); });
        }

        public void SaveUrlToFile(string name, string url)
        {
            string toAppend = oneLine(name) + DataSeperator + oneLine(url);
            File.AppendAllText(myFilePath, toAppend);
        }
    }
}
