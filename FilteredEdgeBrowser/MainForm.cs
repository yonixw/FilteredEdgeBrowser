using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilteredEdgeBrowser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        bool shouldHide = true;
        private void minMaxViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shouldHide = !shouldHide;
            foreach (TabPage tab in tabViews.TabPages)
            {
                (tab.Controls[0] as MyWebTab).SetToolbarVisibility(shouldHide);
            }
        }

        private void addTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWebTab vw = new MyWebTab();
            vw.SetToolbarVisibility(shouldHide);
            vw.Dock = DockStyle.Fill;
            TabPage tab = new TabPage("Loading...");
            tab.Controls.Add(vw);
            tabViews.TabPages.Add(tab);
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabViews.SelectedIndex > -1)
            {
                tabViews.TabPages.RemoveAt(tabViews.SelectedIndex);
            }
        }

        private void closeToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tabIndex = tabViews.SelectedIndex; // remember because might change
            CloseToLeft(tabIndex);
        }

        private void CloseToLeft(int currentIndex)
        {  
            if (currentIndex > -1)
            {
                for (int i = 0; i < currentIndex; i++)
                {
                    tabViews.TabPages.RemoveAt(0);
                }
            }
        }

        private void closeToRightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int tabIndex = tabViews.SelectedIndex;
            CloseToRight(tabIndex);
        }

        private void CloseToRight(int currentIndex)
        {
            
            if (currentIndex > -1)
            {
                for (int i = currentIndex + 1; i < tabViews.TabPages.Count; i++)
                {
                    tabViews.TabPages.RemoveAt(i);
                }
            }
        }

        private void closeOthersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabViews.SelectedIndex > 0)
            {
                CloseToLeft(tabViews.SelectedIndex);
                CloseToRight(0);
            }
        }

        
    }
}
