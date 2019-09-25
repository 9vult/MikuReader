using Newtonsoft.Json;
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

namespace MikuReader
{
    public partial class FrmMangaSettings : Form
    {
        private Manga m;
        public FrmMangaSettings(Manga m)
        {
            InitializeComponent();
            this.m = m;
            dynamic contents = JsonConvert.DeserializeObject(File.ReadAllText(m.mangaDirectory.FullName + "\\manga.json"));
            Setup(contents);
        }

        /// <summary>
        /// Apply existing settings to the window
        /// </summary>
        /// <param name="contents">JSON</param>
        private void Setup(dynamic contents)
        {
            m.LoadSettings();
            // Group Names
            cmboGroup.Items.Add("{Any}");
            cmboGroup.SelectedIndex = 0;
            foreach (var chapterID in contents.chapter)
            {
                foreach (var chapter in chapterID)
                {
                    string group = chapter.group_name;
                    if (!IsGroupInList(group))
                    {
                        cmboGroup.Items.Add(group);
                        if (group.Equals(m.settings.group))
                            cmboGroup.SelectedIndex = cmboGroup.Items.Count - 1;
                    }
                }
            }

            foreach (string localeOption in cmboLang.Items)
            {
                if (localeOption.StartsWith(m.settings.lang))
                {
                    cmboLang.SelectedItem = localeOption;
                    break;
                }
            }

            if (m.settings.name == "" || m.settings.name == null)
            {
                txtName.Text = m.name;
            } else
            {
                txtName.Text = m.settings.name;
            }
        }

        /// <summary>
        /// Checks if the group is already in the list of groups
        /// </summary>
        /// <param name="group">Group name</param>
        /// <returns></returns>
        private bool IsGroupInList(string group)
        {
            for (int i = 0; i < cmboGroup.Items.Count; i++)
            {
                if (cmboGroup.Items[i].ToString() == group)
                {
                    return true;
                }
            }
            return false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void FrmMangaSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // MessageBox.Show("Any changes will be saved.");
            Save();
        }

        private void Save()
        {
            m.SaveSettings(cmboLang.SelectedItem.ToString().Substring(0, 2), cmboGroup.SelectedItem.ToString(), txtName.Text);
            m.LoadSettings();
            DialogResult = DialogResult.OK;
        }

        private void FrmMangaSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }
    }
}
