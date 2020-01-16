namespace MikuReader.wf.Forms
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.rbSingle = new System.Windows.Forms.RadioButton();
            this.rbDouble = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbSingle
            // 
            this.rbSingle.AutoSize = true;
            this.rbSingle.Location = new System.Drawing.Point(6, 25);
            this.rbSingle.Name = "rbSingle";
            this.rbSingle.Size = new System.Drawing.Size(119, 24);
            this.rbSingle.TabIndex = 0;
            this.rbSingle.TabStop = true;
            this.rbSingle.Text = "Single Page";
            this.rbSingle.UseVisualStyleBackColor = true;
            // 
            // rbDouble
            // 
            this.rbDouble.AutoSize = true;
            this.rbDouble.Location = new System.Drawing.Point(6, 55);
            this.rbDouble.Name = "rbDouble";
            this.rbDouble.Size = new System.Drawing.Size(126, 24);
            this.rbDouble.TabIndex = 1;
            this.rbDouble.TabStop = true;
            this.rbDouble.Text = "Double Page";
            this.rbDouble.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSingle);
            this.groupBox1.Controls.Add(this.rbDouble);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 134);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reader";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(364, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 44);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(12, 152);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(121, 44);
            this.btnAbout.TabIndex = 4;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDir);
            this.groupBox2.Controls.Add(this.chkUpdates);
            this.groupBox2.Location = new System.Drawing.Point(186, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 134);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(244, 75);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(41, 36);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "App Directory";
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(6, 82);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(232, 26);
            this.txtDir.TabIndex = 6;
            // 
            // chkUpdates
            // 
            this.chkUpdates.AutoSize = true;
            this.chkUpdates.Location = new System.Drawing.Point(6, 26);
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.Size = new System.Drawing.Size(241, 24);
            this.chkUpdates.TabIndex = 0;
            this.chkUpdates.Text = "Check for updates on startup";
            this.chkUpdates.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 252);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MikuReader Settings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSingle;
        private System.Windows.Forms.RadioButton rbDouble;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDir;
    }
}