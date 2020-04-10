using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader.wf.Forms
{
    public partial class FrmEdit : Form
    {
        private Title title;
        private bool notFirstTime = true;

        public string usertitle;
        public string userlang;
        public string usergroup;

        public FrmEdit(Title title)
        {
            InitializeComponent();
            this.title = title;

            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
        }

        public FrmEdit(Title title, bool notFirstTime)
        {
            InitializeComponent();
            this.title = title;

            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.notFirstTime = notFirstTime;
            btnCancel.Enabled = notFirstTime;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            txtTitle.Text = title.GetUserTitle();

            if (title is Manga m)
            {
                if (m.GetUserGroup() == "^any-group")
                {
                    cmboGroup.Items.Add("{Any}");
                }
                else
                {
                    cmboGroup.Items.Add(m.GetUserGroup());
                }

                cmboLang.Items.Add(m.GetUserLang());
            }
            else
            {
                btnEnable.Enabled = false;
                cmboGroup.Items.Add("n/a - Hentai");
                cmboLang.Items.Add("n/a - Hentai");
            }
            cmboGroup.SelectedIndex = 0;
            cmboLang.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            title.UpdateProperties(txtTitle.Text, cmboLang.SelectedItem.ToString(), cmboGroup.SelectedItem.ToString());
            usertitle = txtTitle.Text;
            userlang = cmboLang.SelectedItem.ToString();
            usergroup = cmboGroup.SelectedItem.ToString();

            if (title is Manga m)
            {
                if (m.GetDLChapters() == null)
                {
                    m.UpdateDLChapters(new string[] { "-1" });
                }
            }
                
            else

            if (notFirstTime)
                MessageBox.Show("Changes saved! Refresh to view.");
            this.Close();
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnEnable_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show("Changing these settings may require a re-download of manga files.\n\n" +
                                "Also, enabling this section may take several moments, depending on your Internet speed.\n\n" +
                                "Continue?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                btnEnable.Enabled = false;

                if (title is Manga m && m is MangaDex)
                {
                    cmboLang.Items.Clear();

                    foreach (string lc in m.GetLangs())
                    {
                        cmboLang.Items.Add(lc);
                    }
                    foreach (string localeOption in cmboLang.Items)
                    {
                        if (localeOption.StartsWith(m.GetUserLang()))
                        {
                            cmboLang.SelectedItem = localeOption;
                            break;
                        }
                    }

                    cmboGroup.Enabled = true;
                    cmboLang.Enabled = true;
                }
                if (title is Manga)
                    btnChapSelect.Enabled = true; // should allow chapter download for any manga
            }
        }

        private void CmboLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (title is Manga m && m is MangaDex)
            {
                cmboGroup.Items.Clear();
                cmboGroup.Items.Add("{Any}");

                foreach (string group in m.GetGroups(cmboLang.SelectedItem.ToString()))
                {
                    cmboGroup.Items.Add(group);
                }

                if (cmboGroup.Items.Contains(m.GetUserGroup()))
                {
                    cmboGroup.SelectedItem = m.GetUserGroup();
                }
                else if (m.GetUserGroup() == "^any-group")
                {
                    cmboGroup.SelectedItem = "{Any}";
                }
                else
                {
                    cmboGroup.SelectedItem = "{Any}";
                }
            }
        }

        private void BtnChapSelect_Click(object sender, EventArgs e)
        {
            if (title is Manga m)
            {
                Chapter[] chapters = m.GetChapters().ToArray();
                FrmChapterSelect chapForm;
                chapForm = new FrmChapterSelect(m.GetSetPrunedChapters(true).ToArray(), m.GetDLChapters());
                var result = chapForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] selected = chapForm.ReturnValue;
                    m.UpdateDLChapters(selected);
                }
            }
        }
    }
}
