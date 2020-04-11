using MikuReader.Core;
using MikuReader.wf.Classes;
using MikuReader.wf.Classes.Region;
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
    public partial class FrmSettings : Form
    {
        private Settings sl;

        public FrmSettings()
        {
            InitializeComponent();

            sl = WFClient.lh.CurrentLanguage.Settings;

            // Language setup
            groupBoxReader.Text = sl.Group_reader;
            groupBoxApplication.Text = sl.Group_application;
            rbSingle.Text = sl.Rb_single;
            rbDouble.Text = sl.Rb_double;
            chkUpdates.Text = sl.Chk_update;
            lblAppDir.Text = sl.Lbl_appdir;
            lblLang.Text = sl.Lbl_lang;
            btnAbout.Text = sl.Btn_about;
            btnSave.Text = sl.Btn_save;
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            SettingsHelper.Initialize();

            if (SettingsHelper.UseDoubleReader)
                rbDouble.Checked = true;
            else
                rbSingle.Checked = true;

            chkUpdates.Checked = SettingsHelper.CheckForUpdates;

            txtDir.Text = (string)Properties.Settings.Default["approot"];

            cmboLanguage.Items.AddRange(WFClient.lh.GetLanguageNames());
            cmboLanguage.SelectedIndex = cmboLanguage.Items.IndexOf(WFClient.lh.CurrentLanguage.Name);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SettingsHelper.UseDoubleReader = rbDouble.Checked;
            SettingsHelper.CheckForUpdates = chkUpdates.Checked;

            if (!txtDir.Text.Equals((string)Properties.Settings.Default["approot"]))
            {
                DialogResult dr = MessageBox.Show(sl.Msg_saveloc, "Confirmation", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (!txtDir.Text.Equals(""))
                    {
                        Properties.Settings.Default["approot"] = txtDir.Text;
                        Properties.Settings.Default.Save();
                    } else
                    {
                        Properties.Settings.Default["approot"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MikuReader2");
                        Properties.Settings.Default.Save();
                    }                    
                } else
                {
                    txtDir.Text = (string)Properties.Settings.Default["approot"];
                }
            }

            if (!cmboLanguage.SelectedItem.ToString().Equals(Properties.Settings.Default["language"].ToString()))
            {
                DialogResult dr = MessageBox.Show(sl.Msg_lang, "Confirmation", MessageBoxButtons.OK);
                Properties.Settings.Default["language"] = cmboLanguage.SelectedItem.ToString();
                Properties.Settings.Default.Save();
            }

            SettingsHelper.Save();
            WFClient.logger.Log("Settings saved.");
            MessageBox.Show(sl.Msg_saved);
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtDir.Text = fbd.SelectedPath;
            }
        }
    }
}
