﻿using FilteredEdgeBrowser.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilteredEdgeBrowser.Dialogs
{
    public class UrlItem
    {
        string _url;
        string _name;

        public UrlItem(string name, string url)
        {
            _name = name;
            _url = url;
        }

        public override string ToString()
        {
            return frmTabHistory.formatURLHelper(_name, _url);
        }

        public string URL() { return _url; }
    }

    public partial class frmEditUrl : Form
    {
        public string URL;

        public frmEditUrl()
        {
            InitializeComponent();
        }

        
        void updateProgress(ProgressBar pb, int value)
        {
            pb.Invoke(new Action(() =>
            {
                pb.Value = Math.Min(100, Math.Max(0, value));
            }));
        }

        void readResult(LogFileHandler handler, ListBox target)
        {
            target.Invoke(new Action(() =>
            {
                target.Items.Clear();
            }));

            for (int i=0;i<handler.resultCount; i++)
            {
                target.Invoke(new Action(() =>
                {
                    UrlItem item = new UrlItem(handler.UrlResults[i, 0], handler.UrlResults[i, 1]);
                    target.Items.Add(item);
                }));
            }
        }

        public void ReturnURL(string url)
        {
            URL = url;
            DialogResult = DialogResult.OK;
        }

        private void frmEditUrl_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            txtURL.Text = URL;

            MainForm.bookmarkLog.onProgressUpdated = (p) => { updateProgress(pbBookmark, p); };
            MainForm.bookmarkLog.onSearchFinish = () => { readResult(MainForm.bookmarkLog, lstBookmark); };

            MainForm.historyLog.onProgressUpdated = (p) => { updateProgress(pbHistory, p); };
            MainForm.historyLog.onSearchFinish = () => { readResult(MainForm.historyLog, lstHistory); };
        }




        private void button1_Click(object sender, EventArgs e)
        {
            ReturnURL(txtURL.Text);
        }


        

        void onTabChange(int index)
        {
            if (txtFreeStyle.Text.Length == 0) return;

            switch (index)
            {
                case 0: // Google
                    break;

                case 1: // History
                    MainForm.historyLog.updateSearchParams(txtFreeStyle.Text);
                    break;

                case 2: // Bookmark
                    MainForm.bookmarkLog.updateSearchParams(txtFreeStyle.Text);
                    break;
            }
        }

        private void txtFreeStyle_TextChanged(object sender, EventArgs e)
        {
            Uri url = null;
            if (Uri.TryCreate("http://" + txtFreeStyle.Text, UriKind.Absolute, out url))
            {
                Uri http = url;
                Uri https = new Uri("https://" + txtFreeStyle.Text);

                lstHTTP.Items.Clear();
                lstHTTP.Items.Add(http.ToString());
                lstHTTP.Items.Add(https.ToString());
            }

            onTabChange(tabControl1.SelectedIndex);
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            onTabChange(tabControl1.SelectedIndex);
        }

        private void lstGoogle_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void lstHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lst = sender as ListBox;
            if (lst.SelectedIndex > -1)
            {
                ReturnURL((lst.SelectedItem as UrlItem).URL());
            }
        }

        private void lstBookmark_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lst = sender as ListBox;
            if (lst.SelectedIndex > -1)
            {
                ReturnURL((lst.SelectedItem as UrlItem).URL());
            }
        }
    }
}
