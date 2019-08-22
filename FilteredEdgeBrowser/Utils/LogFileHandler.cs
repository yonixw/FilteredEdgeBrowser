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

        public void safeArea(Action<Func<T>,Action<T>> callback)
        {
            lock(myLock)
            {
                callback(() => { return data; }, (T newValue) => { data = newValue; });
            }
        }
    }

    class LogFileHandler
    {
        private string oneLine(string input) { return input.Replace("\r", "").Replace("\n", ""); }


        private string myFilePath = "";
        public LogFileHandler(string filepath)
        {
            myFilePath = filepath;
        }

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

        private void mainSearchLoop()
        {
            isThreadRunning.safeArea((get, set) => { set(true); });


            string currentSearch = searchText;
            if (currentSearch ==  null || currentSearch.Length > 0)
            {
                stopSearchFlagUp = true;
            }

            System.IO.StreamReader file = new System.IO.StreamReader(myFilePath);

            string line = file.ReadLine();
            bool isEOF = (line == null);
            bool isThreadStopping = stopSearchFlagUp;
            bool isSearchChanged = (currentSearch == searchText);

            while (!isThreadStopping && !isEOF)
            {
                while (!isEOF && !isThreadStopping && !isSearchChanged)
                {
                    if (line.IndexOf(currentSearch, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        ///////////////
                    }

                    line = file.ReadLine();
                    isEOF = (line == null);
                    isThreadStopping = stopSearchFlagUp;
                    isSearchChanged = (currentSearch == searchText);
                }

                if (!isEOF && !isThreadStopping && isSearchChanged)
                {
                    currentSearch = searchText;
                    if (currentSearch == null || currentSearch.Length > 0)
                    {
                        stopSearchFlagUp = true;
                    }
                    file = new System.IO.StreamReader(myFilePath);

                    line = file.ReadLine();
                    isEOF = (line == null);
                }
            }

            file.Close();
            isThreadRunning.safeArea((get, set) => { set(false); });
        }

        public void SaveUrlToFile (string name, string url)
        {
            string toAppend = oneLine(name) + Environment.NewLine + oneLine(url);
            File.AppendAllText(myFilePath, toAppend);
        }
    }
}
