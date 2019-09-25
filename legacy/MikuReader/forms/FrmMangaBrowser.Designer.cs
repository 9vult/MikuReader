namespace MikuReader
{
    partial class FrmMangaBrowser
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMangaBrowser));
            this.mnuNav = new System.Windows.Forms.MenuStrip();
            this.goBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addThisTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserPanel = new System.Windows.Forms.Panel();
            this.mnuNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuNav
            // 
            this.mnuNav.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuNav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goBackToolStripMenuItem,
            this.addThisTitleToolStripMenuItem});
            this.mnuNav.Location = new System.Drawing.Point(0, 0);
            this.mnuNav.Name = "mnuNav";
            this.mnuNav.Size = new System.Drawing.Size(1730, 33);
            this.mnuNav.TabIndex = 1;
            this.mnuNav.Text = "menuStrip1";
            // 
            // goBackToolStripMenuItem
            // 
            this.goBackToolStripMenuItem.Name = "goBackToolStripMenuItem";
            this.goBackToolStripMenuItem.Size = new System.Drawing.Size(105, 29);
            this.goBackToolStripMenuItem.Text = "< Go Back";
            this.goBackToolStripMenuItem.Click += new System.EventHandler(this.GoBackToolStripMenuItem_Click);
            // 
            // addThisTitleToolStripMenuItem
            // 
            this.addThisTitleToolStripMenuItem.Enabled = false;
            this.addThisTitleToolStripMenuItem.Name = "addThisTitleToolStripMenuItem";
            this.addThisTitleToolStripMenuItem.Size = new System.Drawing.Size(131, 29);
            this.addThisTitleToolStripMenuItem.Text = "Add This Title";
            this.addThisTitleToolStripMenuItem.Click += new System.EventHandler(this.AddThisTitleToolStripMenuItem_Click);
            // 
            // browserPanel
            // 
            this.browserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserPanel.Location = new System.Drawing.Point(0, 33);
            this.browserPanel.Name = "browserPanel";
            this.browserPanel.Size = new System.Drawing.Size(1730, 840);
            this.browserPanel.TabIndex = 2;
            // 
            // FrmMangaBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1730, 873);
            this.Controls.Add(this.browserPanel);
            this.Controls.Add(this.mnuNav);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuNav;
            this.Name = "FrmMangaBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MikuReader | Browse for Manga";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMangaBrowser_FormClosing);
            this.mnuNav.ResumeLayout(false);
            this.mnuNav.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mnuNav;
        private System.Windows.Forms.ToolStripMenuItem goBackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addThisTitleToolStripMenuItem;
        private System.Windows.Forms.Panel browserPanel;
    }
}