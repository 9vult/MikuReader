namespace MikuReader.wf.Forms
{
    partial class FrmChapterSelect
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
            this.lstChapters = new System.Windows.Forms.CheckedListBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstChapters
            // 
            this.lstChapters.FormattingEnabled = true;
            this.lstChapters.Location = new System.Drawing.Point(12, 12);
            this.lstChapters.Name = "lstChapters";
            this.lstChapters.Size = new System.Drawing.Size(330, 277);
            this.lstChapters.TabIndex = 0;
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(348, 12);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(129, 53);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "Select All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(348, 71);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(129, 53);
            this.btnNone.TabIndex = 2;
            this.btnNone.Text = "Select None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(348, 236);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(129, 53);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // FrmChapterSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 305);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.lstChapters);
            this.Name = "FrmChapterSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select chapters for download";
            this.Load += new System.EventHandler(this.FrmChapterSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstChapters;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnDone;
    }
}