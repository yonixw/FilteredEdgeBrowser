﻿using System;
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
        LocalHistoryManager myHistory = new LocalHistoryManager(); 

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

        private void WvMain_NewWindowRequested(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNewWindowRequestedEventArgs e)
        {
            e.Handled = true;  
        }

        private void WvMain_DOMContentLoaded(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlDOMContentLoadedEventArgs e)
        {
            setStatus("DOM Content Loaded");
            myHistory.Navigated(e.Uri, wvMain.DocumentTitle);
            lblTitle.Text = wvMain.DocumentTitle;

            string newURL = e.Uri.ToString();
            txtURL.BackColor = (newURL.StartsWith("https")) ? Color.FromArgb(192,255,192) : Color.FromArgb(255, 192, 192);
            txtURL.Text = newURL;

        }
        
        private void WvMain_NavigationStarting(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationStartingEventArgs e)
        {
            // TODO: Filter here
            setStatus("Navigate to " + e.Uri.ToString());
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
    }
   
}
