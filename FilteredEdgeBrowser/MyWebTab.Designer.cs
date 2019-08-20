namespace FilteredEdgeBrowser
{
    partial class MyWebTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbMenu = new System.Windows.Forms.GroupBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.wvMain = new Microsoft.Toolkit.Forms.UI.Controls.WebView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.navigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wvMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMenu
            // 
            this.gbMenu.Controls.Add(this.txtURL);
            this.gbMenu.Controls.Add(this.menuStrip1);
            this.gbMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMenu.Location = new System.Drawing.Point(0, 0);
            this.gbMenu.Name = "gbMenu";
            this.gbMenu.Size = new System.Drawing.Size(747, 103);
            this.gbMenu.TabIndex = 1;
            this.gbMenu.TabStop = false;
            // 
            // txtURL
            // 
            this.txtURL.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtURL.Location = new System.Drawing.Point(3, 40);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(741, 20);
            this.txtURL.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 572);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(747, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // wvMain
            // 
            this.wvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wvMain.Location = new System.Drawing.Point(0, 103);
            this.wvMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.wvMain.Name = "wvMain";
            this.wvMain.Size = new System.Drawing.Size(747, 469);
            this.wvMain.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 16);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(741, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // navigateToolStripMenuItem
            // 
            this.navigateToolStripMenuItem.Name = "navigateToolStripMenuItem";
            this.navigateToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.navigateToolStripMenuItem.Text = "History";
            this.navigateToolStripMenuItem.Click += new System.EventHandler(this.navigateToolStripMenuItem_Click);
            // 
            // MyWebTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wvMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbMenu);
            this.Name = "MyWebTab";
            this.Size = new System.Drawing.Size(747, 594);
            this.Load += new System.EventHandler(this.MyWebTab_Load);
            this.gbMenu.ResumeLayout(false);
            this.gbMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wvMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMenu;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private Microsoft.Toolkit.Forms.UI.Controls.WebView wvMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem navigateToolStripMenuItem;
    }
}
