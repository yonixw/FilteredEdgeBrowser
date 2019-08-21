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
        public frmEditUrl()
        {
            InitializeComponent();
        }

        private void frmEditUrl_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
        }
    }
}
