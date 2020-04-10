namespace MikuReader.wf.Forms
{
    partial class FrmUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdate));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNewVer = new System.Windows.Forms.Label();
            this.lblCurVer = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtChangelog = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.rbWait = new System.Windows.Forms.RadioButton();
            this.rbUpdate = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(540, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "An update is available!";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNewVer);
            this.groupBox1.Controls.Add(this.lblCurVer);
            this.groupBox1.Location = new System.Drawing.Point(22, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 96);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // lblNewVer
            // 
            this.lblNewVer.AutoSize = true;
            this.lblNewVer.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVer.Location = new System.Drawing.Point(6, 51);
            this.lblNewVer.Name = "lblNewVer";
            this.lblNewVer.Size = new System.Drawing.Size(97, 29);
            this.lblNewVer.TabIndex = 1;
            this.lblNewVer.Text = "Update to";
            // 
            // lblCurVer
            // 
            this.lblCurVer.AutoSize = true;
            this.lblCurVer.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurVer.Location = new System.Drawing.Point(6, 22);
            this.lblCurVer.Name = "lblCurVer";
            this.lblCurVer.Size = new System.Drawing.Size(151, 29);
            this.lblCurVer.TabIndex = 0;
            this.lblCurVer.Text = "Current Version";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtChangelog);
            this.groupBox2.Location = new System.Drawing.Point(22, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 290);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Changelog";
            // 
            // txtChangelog
            // 
            this.txtChangelog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChangelog.Location = new System.Drawing.Point(3, 22);
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ReadOnly = true;
            this.txtChangelog.Size = new System.Drawing.Size(524, 265);
            this.txtChangelog.TabIndex = 0;
            this.txtChangelog.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnContinue);
            this.groupBox3.Controls.Add(this.rbWait);
            this.groupBox3.Controls.Add(this.rbUpdate);
            this.groupBox3.Location = new System.Drawing.Point(22, 463);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(527, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Actions";
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(395, 25);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(126, 69);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.BtnContinue_Click);
            // 
            // rbWait
            // 
            this.rbWait.AutoSize = true;
            this.rbWait.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWait.Location = new System.Drawing.Point(6, 61);
            this.rbWait.Name = "rbWait";
            this.rbWait.Size = new System.Drawing.Size(142, 33);
            this.rbWait.TabIndex = 1;
            this.rbWait.Text = "I Can Wait...";
            this.rbWait.UseVisualStyleBackColor = true;
            // 
            // rbUpdate
            // 
            this.rbUpdate.AutoSize = true;
            this.rbUpdate.Checked = true;
            this.rbUpdate.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUpdate.Location = new System.Drawing.Point(6, 25);
            this.rbUpdate.Name = "rbUpdate";
            this.rbUpdate.Size = new System.Drawing.Size(244, 33);
            this.rbUpdate.TabIndex = 0;
            this.rbUpdate.TabStop = true;
            this.rbUpdate.Text = "Download Update Now!";
            this.rbUpdate.UseVisualStyleBackColor = true;
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 591);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(597, 647);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(597, 647);
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MikuReader Update Service";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNewVer;
        private System.Windows.Forms.Label lblCurVer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtChangelog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.RadioButton rbWait;
        private System.Windows.Forms.RadioButton rbUpdate;
    }
}