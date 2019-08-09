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
    public partial class FrmSettings : Form
    {
        private FrmStartPage startPage;

        public FrmSettings(FrmStartPage startPage)
        {
            InitializeComponent();
            this.startPage = startPage;
        }

        /// <summary>
        /// Open a FileDialog for the user to select a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                SelectedPath = txtDirectory.Text
            };
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDirectory.Text = fbd.SelectedPath;
            }
        }

        /// <summary>
        /// Load the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            chkUpdates.Checked = (bool)Properties.Settings.Default["checkForUpdates"];
            chkDblReader.Checked = (bool)Properties.Settings.Default["doublePageReader"];
            txtDirectory.Text = (string)Properties.Settings.Default["homeDirectory"];

            string savedLocale = (string)Properties.Settings.Default["languageCode"]; 
            foreach (string localeOption in cmboLang.Items)
            {
                if (localeOption.StartsWith(savedLocale))
                {
                    cmboLang.SelectedItem = localeOption;
                    break;
                }
            }
        }

        private void BtnViewFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", txtDirectory.Text);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }

        /// <summary>
        /// Save, checking if the user changed the directory, and warning if it is the case.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Save Update Checking preference 
            Properties.Settings.Default["checkForUpdates"] = chkUpdates.Checked;

            // Save Double-Page Reader preference
            Properties.Settings.Default["doublePageReader"] = chkDblReader.Checked;

            // Save language preference
            Properties.Settings.Default["languageCode"] = cmboLang.SelectedItem.ToString().Substring(0, 2);

            // Check if the home directory was changed
            if (!(new DirectoryInfo(txtDirectory.Text).ToString().Equals(new DirectoryInfo((string)Properties.Settings.Default["homeDirectory"]).ToString())))
            {
                DialogResult result = MessageBox.Show("Changing the save directory will require a restart. Continue?", "MikuReader", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Properties.Settings.Default["homeDirectory"] = txtDirectory.Text;
                    Properties.Settings.Default.Save();
                    Application.Restart();
                } else
                {
                    txtDirectory.Text = (string)Properties.Settings.Default["homeDirectory"];
                }
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("Requested changes were saved", "MikuReader");
            startPage.RefreshContents();
        }
    }
}
