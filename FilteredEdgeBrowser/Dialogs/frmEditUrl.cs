using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilteredEdgeBrowser.Dialogs
{
    public partial class frmEditUrl : Form
    {
        public string URL;

        public frmEditUrl()
        {
            InitializeComponent();
        }

        private void frmEditUrl_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            txtURL.Text = URL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            URL = txtURL.Text;
            DialogResult = DialogResult.OK;
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
            else
            {
                // Google suggestion
            }

            // TODO: history\bookmar threads
        }
    }
}
