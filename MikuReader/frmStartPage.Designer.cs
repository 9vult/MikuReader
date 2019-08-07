namespace MikuReader
{
    partial class FrmStartPage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStartPage));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lstNames = new System.Windows.Forms.ListBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnGlobalSettings = new System.Windows.Forms.Button();
            this.lblDownloading = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMangaSettings = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cabin", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(274, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 44);
            this.label1.TabIndex = 1;
            this.label1.Text = "9volt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cabin", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(270, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 68);
            this.label2.TabIndex = 2;
            this.label2.Text = "MikuReader";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(282, 343);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(190, 47);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse For Manga";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(282, 396);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(190, 32);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh All";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lstNames
            // 
            this.lstNames.Font = new System.Drawing.Font("Menlo", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstNames.FormattingEnabled = true;
            this.lstNames.ItemHeight = 19;
            this.lstNames.Location = new System.Drawing.Point(282, 124);
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(386, 213);
            this.lstNames.TabIndex = 4;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(478, 343);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(190, 47);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.BtnRead_Click);
            // 
            // btnGlobalSettings
            // 
            this.btnGlobalSettings.Location = new System.Drawing.Point(478, 434);
            this.btnGlobalSettings.Name = "btnGlobalSettings";
            this.btnGlobalSettings.Size = new System.Drawing.Size(190, 32);
            this.btnGlobalSettings.TabIndex = 6;
            this.btnGlobalSettings.Text = "Global Settings";
            this.btnGlobalSettings.UseVisualStyleBackColor = true;
            this.btnGlobalSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // lblDownloading
            // 
            this.lblDownloading.AutoSize = true;
            this.lblDownloading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownloading.ForeColor = System.Drawing.Color.Red;
            this.lblDownloading.Location = new System.Drawing.Point(502, 13);
            this.lblDownloading.Name = "lblDownloading";
            this.lblDownloading.Size = new System.Drawing.Size(166, 40);
            this.lblDownloading.TabIndex = 10;
            this.lblDownloading.Text = "Downloading...\r\nPlease do not close";
            this.lblDownloading.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblDownloading.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // currentProgress
            // 
            this.currentProgress.Location = new System.Drawing.Point(379, 13);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(155, 20);
            this.currentProgress.TabIndex = 11;
            this.currentProgress.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MikuReader.Properties.Resources.miku;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 454);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnMangaSettings
            // 
            this.btnMangaSettings.Location = new System.Drawing.Point(478, 396);
            this.btnMangaSettings.Name = "btnMangaSettings";
            this.btnMangaSettings.Size = new System.Drawing.Size(190, 32);
            this.btnMangaSettings.TabIndex = 12;
            this.btnMangaSettings.Text = "Selected Settings";
            this.btnMangaSettings.UseVisualStyleBackColor = true;
            this.btnMangaSettings.Click += new System.EventHandler(this.BtnMangaSettings_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(282, 433);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(190, 32);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update All";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // FrmStartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 477);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnMangaSettings);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.lblDownloading);
            this.Controls.Add(this.btnGlobalSettings);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.lstNames);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmStartPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MikuReader /Beta/";
            this.Load += new System.EventHandler(this.FrmStartPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox lstNames;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnGlobalSettings;
        private System.Windows.Forms.Label lblDownloading;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar currentProgress;
        private System.Windows.Forms.Button btnMangaSettings;
        private System.Windows.Forms.Button btnUpdate;
    }
}

