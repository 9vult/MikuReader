using MikuReader.Core;
using MikuReader.wf.Classes;
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
        public FrmSettings()
        {
            InitializeComponent();
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
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SettingsHelper.UseDoubleReader = rbDouble.Checked;
            SettingsHelper.CheckForUpdates = chkUpdates.Checked;

            if (!txtDir.Text.Equals((string)Properties.Settings.Default["approot"]))
            {
                DialogResult dr = MessageBox.Show("Changing the default save location may have undesirable side effects." +
                    "\n\nAre you sure?",
                    "Confirmation",
                    MessageBoxButtons.YesNo);
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


            SettingsHelper.Save();
            WFClient.logger.Log("Settings saved.");
            MessageBox.Show("All settings saved.");
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
