namespace MikuReader
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
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboLang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnChangeDirectory = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnViewFolder = new System.Windows.Forms.Button();
            this.chkDblReader = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkUpdates
            // 
            this.chkUpdates.AutoSize = true;
            this.chkUpdates.Location = new System.Drawing.Point(12, 12);
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.Size = new System.Drawing.Size(247, 24);
            this.chkUpdates.TabIndex = 0;
            this.chkUpdates.Text = "Check for Updates on Launch";
            this.chkUpdates.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(377, 299);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(278, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Default Manga lang";
            // 
            // cmboLang
            // 
            this.cmboLang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmboLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmboLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboLang.Font = new System.Drawing.Font("Menlo", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboLang.FormattingEnabled = true;
            this.cmboLang.Items.AddRange(new object[] {
            "sa | Arabic",
            "bd | Bengali",
            "bg | Bulgarian",
            "mm | Burmese",
            "ct | Catalan",
            "cn | Chinese (Simp)",
            "hk | Chinese (Trad)",
            "cz | Czech",
            "dk | Danish",
            "nl | Dutch",
            "gb | English",
            "ph | Filipino",
            "fi | Finnish",
            "fr | French",
            "de | German",
            "gr | Greek",
            "hu | Hungarian",
            "id | Indonesian",
            "it | Italian",
            "jp | Japanese",
            "kr | Korean",
            "my | Malay",
            "mn | Mongolian",
            "ir | Persian",
            "pl | Polish",
            "br | Portuguese (Br)",
            "pt | Portuguese (Pt)",
            "ro | Romanian",
            "ru | Russian",
            "rs | Serbo-Croatian",
            "es | Spanish (Es)",
            "mx | Spanish (LATAM)",
            "se | Swedish",
            "th | Thai",
            "tr | Turkish",
            "ua | Ukrainian",
            "vn | Vietnamese"});
            this.cmboLang.Location = new System.Drawing.Point(177, 51);
            this.cmboLang.Name = "cmboLang";
            this.cmboLang.Size = new System.Drawing.Size(293, 27);
            this.cmboLang.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Global Save Directory";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Enabled = false;
            this.txtDirectory.Location = new System.Drawing.Point(176, 89);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(249, 26);
            this.txtDirectory.TabIndex = 6;
            // 
            // btnChangeDirectory
            // 
            this.btnChangeDirectory.Location = new System.Drawing.Point(431, 85);
            this.btnChangeDirectory.Name = "btnChangeDirectory";
            this.btnChangeDirectory.Size = new System.Drawing.Size(38, 30);
            this.btnChangeDirectory.TabIndex = 7;
            this.btnChangeDirectory.Text = "...";
            this.btnChangeDirectory.UseVisualStyleBackColor = true;
            this.btnChangeDirectory.Click += new System.EventHandler(this.BtnChangeDirectory_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(446, 60);
            this.label3.TabIndex = 8;
            this.label3.Text = "Note:\r\n- Changes to Language Code will only affect new downloads.\r\n- Changes to S" +
    "ave Directory will require MikuReader to restart.";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(12, 299);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(93, 32);
            this.btnAbout.TabIndex = 9;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // btnViewFolder
            // 
            this.btnViewFolder.Location = new System.Drawing.Point(348, 121);
            this.btnViewFolder.Name = "btnViewFolder";
            this.btnViewFolder.Size = new System.Drawing.Size(121, 32);
            this.btnViewFolder.TabIndex = 10;
            this.btnViewFolder.Text = "View Folder";
            this.btnViewFolder.UseVisualStyleBackColor = true;
            this.btnViewFolder.Click += new System.EventHandler(this.BtnViewFolder_Click);
            // 
            // chkDblReader
            // 
            this.chkDblReader.AutoSize = true;
            this.chkDblReader.Location = new System.Drawing.Point(12, 158);
            this.chkDblReader.Name = "chkDblReader";
            this.chkDblReader.Size = new System.Drawing.Size(322, 24);
            this.chkDblReader.TabIndex = 11;
            this.chkDblReader.Text = "Use Double-Page Reader (experimental)";
            this.chkDblReader.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 343);
            this.Controls.Add(this.chkDblReader);
            this.Controls.Add(this.btnViewFolder);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnChangeDirectory);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboLang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkUpdates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MikuReader Settings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboLang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button btnChangeDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnViewFolder;
        private System.Windows.Forms.CheckBox chkDblReader;
    }
}