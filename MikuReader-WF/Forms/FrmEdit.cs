using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader.wf.Forms
{
    public partial class FrmEdit : Form
    {
        private Title title;

        public FrmEdit(Title title)
        {
            InitializeComponent();
            this.title = title;
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            txtTitle.Text = title.GetUserTitle();

            if (title is Manga)
            {
                Manga m = (Manga)title;
                if (m.GetUserGroup == "^any-group")
                {
                    cmboGroup.Items.Add("{Any}");
                }
                else
                {
                    cmboGroup.Items.Add(m.GetUserGroup);
                }

                cmboLang.Items.Add(m.GetUserLang);
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

                if (title is Manga)
                {
                    Manga m = (Manga)title;
                    cmboLang.Items.Clear();

                    foreach (string lc in m.GetLangs())
                    {
                        cmboLang.Items.Add(lc);
                    }
                    foreach (string localeOption in cmboLang.Items)
                    {
                        if (localeOption.StartsWith(m.GetUserLang))
                        {
                            cmboLang.SelectedItem = localeOption;
                            break;
                        }
                    }

                    cmboGroup.Enabled = true;
                    cmboLang.Enabled = true;
                }
            }
        }

        private void CmboLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (title is Manga)
            {
                Manga m = (Manga)title;

                cmboGroup.Items.Clear();
                cmboGroup.Items.Add("{Any}");

                foreach (string group in m.GetGroups(cmboLang.SelectedItem.ToString()))
                {
                    cmboGroup.Items.Add(group);
                }

                if (cmboGroup.Items.Contains(m.GetUserGroup))
                {
                    cmboGroup.SelectedItem = m.GetUserGroup;
                }
                else if (m.GetUserGroup == "^any-group")
                {
                    cmboGroup.SelectedItem = "{Any}";
                }
                else
                {
                    cmboGroup.SelectedItem = "{Any}";
                }
            }
        }
    }
}
