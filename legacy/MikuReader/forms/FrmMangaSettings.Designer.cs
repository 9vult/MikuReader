namespace MikuReader
{
    partial class FrmMangaSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMangaSettings));
            this.cmboGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboLang = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmboGroup
            // 
            this.cmboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboGroup.FormattingEnabled = true;
            this.cmboGroup.Location = new System.Drawing.Point(298, 14);
            this.cmboGroup.Name = "cmboGroup";
            this.cmboGroup.Size = new System.Drawing.Size(339, 28);
            this.cmboGroup.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Only download chapters from group";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Only download chapters with language";
            // 
            // cmboLang
            // 
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
            this.cmboLang.Location = new System.Drawing.Point(298, 58);
            this.cmboLang.Name = "cmboLang";
            this.cmboLang.Size = new System.Drawing.Size(339, 27);
            this.cmboLang.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(505, 147);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 39);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(298, 104);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(339, 26);
            this.txtName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Override Title";
            // 
            // FrmMangaSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 198);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboLang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMangaSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manga Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMangaSettings_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMangaSettings_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboLang;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
    }
}