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
            if (SettingsHelper.UseDoubleReader)
                rbDouble.Checked = true;
            else
                rbSingle.Checked = true;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (rbDouble.Checked)
                SettingsHelper.UseDoubleReader = true;
            else
                SettingsHelper.UseDoubleReader = false;

            SettingsHelper.Save();
            MessageBox.Show("All settings saved.");
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }
    }
}
