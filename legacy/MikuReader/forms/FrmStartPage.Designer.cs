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
            this.btnBrowseManga = new System.Windows.Forms.Button();
            this.btnRefreshManga = new System.Windows.Forms.Button();
            this.lstManga = new System.Windows.Forms.ListBox();
            this.btnReadManga = new System.Windows.Forms.Button();
            this.btnGlobalMangaSettings = new System.Windows.Forms.Button();
            this.lblDownloading = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.btnIndMangaSettings = new System.Windows.Forms.Button();
            this.btnUpdateManga = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpManga = new System.Windows.Forms.TabPage();
            this.tpHentai = new System.Windows.Forms.TabPage();
            this.lstHentai = new System.Windows.Forms.ListBox();
            this.btnBrowseHentai = new System.Windows.Forms.Button();
            this.btnRefreshHentai = new System.Windows.Forms.Button();
            this.btnReadHentai = new System.Windows.Forms.Button();
            this.btnHentaiSettings = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tpManga.SuspendLayout();
            this.tpHentai.SuspendLayout();
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
            // btnBrowseManga
            // 
            this.btnBrowseManga.Location = new System.Drawing.Point(3, 283);
            this.btnBrowseManga.Name = "btnBrowseManga";
            this.btnBrowseManga.Size = new System.Drawing.Size(190, 47);
            this.btnBrowseManga.TabIndex = 2;
            this.btnBrowseManga.Text = "Browse For Manga";
            this.btnBrowseManga.UseVisualStyleBackColor = true;
            this.btnBrowseManga.Click += new System.EventHandler(this.BtnBrowseManga_Click);
            // 
            // btnRefreshManga
            // 
            this.btnRefreshManga.Location = new System.Drawing.Point(3, 336);
            this.btnRefreshManga.Name = "btnRefreshManga";
            this.btnRefreshManga.Size = new System.Drawing.Size(190, 32);
            this.btnRefreshManga.TabIndex = 3;
            this.btnRefreshManga.Text = "Refresh All";
            this.btnRefreshManga.UseVisualStyleBackColor = true;
            this.btnRefreshManga.Click += new System.EventHandler(this.BtnRefreshManga_Click);
            // 
            // lstManga
            // 
            this.lstManga.Font = new System.Drawing.Font("Menlo", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstManga.FormattingEnabled = true;
            this.lstManga.ItemHeight = 19;
            this.lstManga.Location = new System.Drawing.Point(3, 6);
            this.lstManga.Name = "lstManga";
            this.lstManga.Size = new System.Drawing.Size(386, 270);
            this.lstManga.TabIndex = 4;
            // 
            // btnReadManga
            // 
            this.btnReadManga.Location = new System.Drawing.Point(199, 283);
            this.btnReadManga.Name = "btnReadManga";
            this.btnReadManga.Size = new System.Drawing.Size(190, 47);
            this.btnReadManga.TabIndex = 1;
            this.btnReadManga.Text = "Read";
            this.btnReadManga.UseVisualStyleBackColor = true;
            this.btnReadManga.Click += new System.EventHandler(this.BtnReadManga_Click);
            // 
            // btnGlobalMangaSettings
            // 
            this.btnGlobalMangaSettings.Location = new System.Drawing.Point(199, 374);
            this.btnGlobalMangaSettings.Name = "btnGlobalMangaSettings";
            this.btnGlobalMangaSettings.Size = new System.Drawing.Size(190, 32);
            this.btnGlobalMangaSettings.TabIndex = 6;
            this.btnGlobalMangaSettings.Text = "Settings";
            this.btnGlobalMangaSettings.UseVisualStyleBackColor = true;
            this.btnGlobalMangaSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // lblDownloading
            // 
            this.lblDownloading.AutoSize = true;
            this.lblDownloading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownloading.ForeColor = System.Drawing.Color.Red;
            this.lblDownloading.Location = new System.Drawing.Point(522, 12);
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
            this.currentProgress.Size = new System.Drawing.Size(176, 20);
            this.currentProgress.TabIndex = 11;
            this.currentProgress.Visible = false;
            // 
            // btnIndMangaSettings
            // 
            this.btnIndMangaSettings.Location = new System.Drawing.Point(199, 336);
            this.btnIndMangaSettings.Name = "btnIndMangaSettings";
            this.btnIndMangaSettings.Size = new System.Drawing.Size(190, 32);
            this.btnIndMangaSettings.TabIndex = 12;
            this.btnIndMangaSettings.Text = "Selected Settings";
            this.btnIndMangaSettings.UseVisualStyleBackColor = true;
            this.btnIndMangaSettings.Click += new System.EventHandler(this.BtnIndMangaSettings_Click);
            // 
            // btnUpdateManga
            // 
            this.btnUpdateManga.Location = new System.Drawing.Point(3, 373);
            this.btnUpdateManga.Name = "btnUpdateManga";
            this.btnUpdateManga.Size = new System.Drawing.Size(190, 32);
            this.btnUpdateManga.TabIndex = 13;
            this.btnUpdateManga.Text = "Update All";
            this.btnUpdateManga.UseVisualStyleBackColor = true;
            this.btnUpdateManga.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpManga);
            this.tabControl1.Controls.Add(this.tpHentai);
            this.tabControl1.Location = new System.Drawing.Point(282, 124);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(406, 445);
            this.tabControl1.TabIndex = 14;
            // 
            // tpManga
            // 
            this.tpManga.Controls.Add(this.lstManga);
            this.tpManga.Controls.Add(this.btnUpdateManga);
            this.tpManga.Controls.Add(this.btnBrowseManga);
            this.tpManga.Controls.Add(this.btnIndMangaSettings);
            this.tpManga.Controls.Add(this.btnRefreshManga);
            this.tpManga.Controls.Add(this.btnReadManga);
            this.tpManga.Controls.Add(this.btnGlobalMangaSettings);
            this.tpManga.Location = new System.Drawing.Point(4, 29);
            this.tpManga.Name = "tpManga";
            this.tpManga.Padding = new System.Windows.Forms.Padding(3);
            this.tpManga.Size = new System.Drawing.Size(398, 412);
            this.tpManga.TabIndex = 0;
            this.tpManga.Text = "MangaDex";
            this.tpManga.UseVisualStyleBackColor = true;
            // 
            // tpHentai
            // 
            this.tpHentai.Controls.Add(this.lstHentai);
            this.tpHentai.Controls.Add(this.btnBrowseHentai);
            this.tpHentai.Controls.Add(this.btnRefreshHentai);
            this.tpHentai.Controls.Add(this.btnReadHentai);
            this.tpHentai.Controls.Add(this.btnHentaiSettings);
            this.tpHentai.Location = new System.Drawing.Point(4, 29);
            this.tpHentai.Name = "tpHentai";
            this.tpHentai.Padding = new System.Windows.Forms.Padding(3);
            this.tpHentai.Size = new System.Drawing.Size(398, 412);
            this.tpHentai.TabIndex = 1;
            this.tpHentai.Text = "nHentai";
            this.tpHentai.UseVisualStyleBackColor = true;
            // 
            // lstHentai
            // 
            this.lstHentai.Font = new System.Drawing.Font("Menlo", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHentai.FormattingEnabled = true;
            this.lstHentai.ItemHeight = 19;
            this.lstHentai.Location = new System.Drawing.Point(6, 6);
            this.lstHentai.Name = "lstHentai";
            this.lstHentai.Size = new System.Drawing.Size(386, 270);
            this.lstHentai.TabIndex = 17;
            // 
            // btnBrowseHentai
            // 
            this.btnBrowseHentai.Location = new System.Drawing.Point(6, 283);
            this.btnBrowseHentai.Name = "btnBrowseHentai";
            this.btnBrowseHentai.Size = new System.Drawing.Size(190, 47);
            this.btnBrowseHentai.TabIndex = 15;
            this.btnBrowseHentai.Text = "Browse For Hentai";
            this.btnBrowseHentai.UseVisualStyleBackColor = true;
            this.btnBrowseHentai.Click += new System.EventHandler(this.BtnBrowseHentai_Click);
            // 
            // btnRefreshHentai
            // 
            this.btnRefreshHentai.Location = new System.Drawing.Point(6, 336);
            this.btnRefreshHentai.Name = "btnRefreshHentai";
            this.btnRefreshHentai.Size = new System.Drawing.Size(386, 32);
            this.btnRefreshHentai.TabIndex = 16;
            this.btnRefreshHentai.Text = "Refresh All";
            this.btnRefreshHentai.UseVisualStyleBackColor = true;
            this.btnRefreshHentai.Click += new System.EventHandler(this.BtnRefreshHentai_Click);
            // 
            // btnReadHentai
            // 
            this.btnReadHentai.Location = new System.Drawing.Point(202, 283);
            this.btnReadHentai.Name = "btnReadHentai";
            this.btnReadHentai.Size = new System.Drawing.Size(190, 47);
            this.btnReadHentai.TabIndex = 14;
            this.btnReadHentai.Text = "Read";
            this.btnReadHentai.UseVisualStyleBackColor = true;
            this.btnReadHentai.Click += new System.EventHandler(this.BtnReadHentai_Click);
            // 
            // btnHentaiSettings
            // 
            this.btnHentaiSettings.Location = new System.Drawing.Point(3, 374);
            this.btnHentaiSettings.Name = "btnHentaiSettings";
            this.btnHentaiSettings.Size = new System.Drawing.Size(389, 32);
            this.btnHentaiSettings.TabIndex = 18;
            this.btnHentaiSettings.Text = "Hentai Settings";
            this.btnHentaiSettings.UseVisualStyleBackColor = true;
            this.btnHentaiSettings.Click += new System.EventHandler(this.BtnHentaiSettings_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MikuReader.Properties.Resources.miku;
            this.pictureBox1.Location = new System.Drawing.Point(20, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 553);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmStartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 584);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.lblDownloading);
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
            this.tabControl1.ResumeLayout(false);
            this.tpManga.ResumeLayout(false);
            this.tpHentai.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseManga;
        private System.Windows.Forms.Button btnRefreshManga;
        private System.Windows.Forms.ListBox lstManga;
        private System.Windows.Forms.Button btnReadManga;
        private System.Windows.Forms.Button btnGlobalMangaSettings;
        private System.Windows.Forms.Label lblDownloading;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar currentProgress;
        private System.Windows.Forms.Button btnIndMangaSettings;
        private System.Windows.Forms.Button btnUpdateManga;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpManga;
        private System.Windows.Forms.TabPage tpHentai;
        private System.Windows.Forms.ListBox lstHentai;
        private System.Windows.Forms.Button btnBrowseHentai;
        private System.Windows.Forms.Button btnRefreshHentai;
        private System.Windows.Forms.Button btnReadHentai;
        private System.Windows.Forms.Button btnHentaiSettings;
    }
}

