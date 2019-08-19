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

        // Invoke script Exception from HRESULT: 0x80020101 ==> JS Syntax error
        //http://suggestqueries.google.com/complete/search?output=toolbar&hl=he&q=%D7%91%D7%99%D7%91&gl=IL

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
        }

        private void WvMain_NewWindowRequested(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            
        }

        public void Navigate(string url)
        {
            wvMain.Navigate(url);
        }

        public void setStatus(string text)
        {
            lblStatus.Text = string.Format("[{0}] {1}", DateTime.Now, text);
        }

        private void WvMain_DOMContentLoaded(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlDOMContentLoadedEventArgs e)
        {
            setStatus("DOM Content Loaded");
        }

        private void WvMain_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e)
        {
            // Final event per page
            setStatus("Navigation Complete");
        }

        private void WvMain_NavigationStarting(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationStartingEventArgs e)
        {
            // TODO: Filter here
            setStatus("Navigate to " + e.Uri.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wvMain.Navigate(new Uri(txtURL.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(wvMain.InvokeScript("eval", "document.body.innerHTML"));
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
