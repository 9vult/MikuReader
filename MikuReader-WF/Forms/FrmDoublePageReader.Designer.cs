namespace MikuReader.wf.Forms
{
    partial class FrmDoublePageReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDoublePageReader));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmboChapter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmboPage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.btnIncrementOne = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbLeft = new System.Windows.Forms.PictureBox();
            this.pbRight = new System.Windows.Forms.PictureBox();
            this.btnDecrementOne = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmboChapter,
            this.toolStripLabel2,
            this.cmboPage,
            this.toolStripSeparator1,
            this.progressBar,
            this.toolStripLabel3,
            this.btnIncrementOne,
            this.btnDecrementOne});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1608, 33);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 30);
            this.toolStripLabel1.Text = "Chapter";
            // 
            // cmboChapter
            // 
            this.cmboChapter.Name = "cmboChapter";
            this.cmboChapter.Size = new System.Drawing.Size(200, 33);
            this.cmboChapter.SelectedIndexChanged += new System.EventHandler(this.CmboChapter_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(50, 30);
            this.toolStripLabel2.Text = "Page";
            // 
            // cmboPage
            // 
            this.cmboPage.Name = "cmboPage";
            this.cmboPage.Size = new System.Drawing.Size(200, 33);
            this.cmboPage.SelectedIndexChanged += new System.EventHandler(this.CmboPage_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.Name = "progressBar";
            this.progressBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.progressBar.Size = new System.Drawing.Size(100, 30);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(81, 30);
            this.toolStripLabel3.Text = "Progress";
            // 
            // btnIncrementOne
            // 
            this.btnIncrementOne.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnIncrementOne.Image = ((System.Drawing.Image)(resources.GetObject("btnIncrementOne.Image")));
            this.btnIncrementOne.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIncrementOne.Name = "btnIncrementOne";
            this.btnIncrementOne.Size = new System.Drawing.Size(38, 30);
            this.btnIncrementOne.Text = "+1";
            this.btnIncrementOne.ToolTipText = "Go up 1 page to offset the pages by 1";
            this.btnIncrementOne.Click += new System.EventHandler(this.BtnIncrementOne_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pbLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pbRight);
            this.splitContainer1.Size = new System.Drawing.Size(1608, 957);
            this.splitContainer1.SplitterDistance = 804;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 2;
            // 
            // pbLeft
            // 
            this.pbLeft.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLeft.Location = new System.Drawing.Point(0, 0);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(804, 957);
            this.pbLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLeft.TabIndex = 0;
            this.pbLeft.TabStop = false;
            this.pbLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pb_MouseDown);
            // 
            // pbRight
            // 
            this.pbRight.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbRight.Location = new System.Drawing.Point(0, 0);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(803, 957);
            this.pbRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbRight.TabIndex = 0;
            this.pbRight.TabStop = false;
            this.pbRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pb_MouseDown);
            // 
            // btnDecrementOne
            // 
            this.btnDecrementOne.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDecrementOne.Image = ((System.Drawing.Image)(resources.GetObject("btnDecrementOne.Image")));
            this.btnDecrementOne.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDecrementOne.Name = "btnDecrementOne";
            this.btnDecrementOne.Size = new System.Drawing.Size(33, 30);
            this.btnDecrementOne.Text = "-1";
            this.btnDecrementOne.ToolTipText = "Go down 1 page to offset the pages by 1";
            this.btnDecrementOne.Click += new System.EventHandler(this.BtnDecrementOne_Click);
            // 
            // FrmDoublePageReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1608, 990);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDoublePageReader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MikuReader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDoublePageReader_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDoublePageReader_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmboChapter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cmboPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pbLeft;
        private System.Windows.Forms.PictureBox pbRight;
        private System.Windows.Forms.ToolStripButton btnIncrementOne;
        private System.Windows.Forms.ToolStripButton btnDecrementOne;
    }
}