using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilteredEdgeBrowser
{
    public partial class MyWebTab : UserControl
    {
        TabPage myPage;
        LocalHistoryManager myHistory = new LocalHistoryManager();

        public delegate void titleEvent(TabPage page, string title);
        public event titleEvent onTitleChange;


        public MyWebTab()
        {
            InitializeComponent();
        }

        public string DocumentTitle { get { return wvMain.DocumentTitle; } }

        private void MyWebTab_Load(object sender, EventArgs e)
        {
            wvMain.NavigationStarting += WvMain_NavigationStarting;
            wvMain.NavigationCompleted += WvMain_NavigationCompleted;
            wvMain.DOMContentLoaded += WvMain_DOMContentLoaded;
            wvMain.NewWindowRequested += WvMain_NewWindowRequested;

            wvMain.Navigate("https://www.google.com");
        }

        public void setStatus(string text)
        {
            lblStatus.Text = string.Format("[{0}] {1}", DateTime.Now, text);
        }

        public void setMyPage(TabPage page)
        {
            myPage = page;
        }

        public void closePage()
        {
            wvMain.Dispose();
        }

        private void WvMain_NewWindowRequested(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNewWindowRequestedEventArgs e)
        {
            e.Handled = true;  
        }

        private void WvMain_DOMContentLoaded(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlDOMContentLoadedEventArgs e)
        {
            Uri currentUri = (e.Uri != null) ? e.Uri : new Uri("https://filtered.content/");

            myHistory.Navigated(currentUri, wvMain.DocumentTitle);
            string newURL = currentUri.ToString();
            txtURL.BackColor = (newURL.StartsWith("https")) ? Color.FromArgb(192, 255, 192) : Color.FromArgb(255, 192, 192);
            txtURL.Text = newURL;
            
            lblTitle.Text = wvMain.DocumentTitle;
            onTitleChange?.Invoke(myPage, wvMain.DocumentTitle);

            setStatus("DOM Content Loaded");
        }
        
        private void WvMain_NavigationStarting(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationStartingEventArgs e)
        {
            if (e.Uri != null)
            {
                // TODO: Filter here
                setStatus("Navigate to " + e.Uri.ToString());
            }
        }

        private void navigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogs.frmTabHistory chooseHistory = new Dialogs.frmTabHistory(myHistory);
            if (chooseHistory.ShowDialog() == DialogResult.OK)
            {
                wvMain.Navigate(chooseHistory.URL);
            }
        }

        private void WvMain_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e)
        {
            // Final event per page
            setStatus("Navigation Complete");
        }

        public void SetToolbarVisibility(bool visible)
        {
            gbMenu.Visible = visible;
        }

        private void bookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Dialogs.frmDlgBookmark(wvMain.DocumentTitle, myHistory.CurrentURL())).ShowDialog();
        }

        private void changeUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogs.frmEditUrl dialog = new Dialogs.frmEditUrl();
            dialog.URL = txtURL.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                wvMain.Navigate(dialog.URL);
            }
        }

        private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wvMain.NavigateToString("<html><head><title>123</title></head><body>Helloe<body></html>");
        }
    }
   
}
