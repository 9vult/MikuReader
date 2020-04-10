using MikuReader.wf.Classes;
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
    public partial class FrmUpdate : Form
    {
        private UpdateInfo info;

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

        public FrmUpdate(UpdateInfo info)
        {
            InitializeComponent();

            lblCurVer.Text = "Current Version: " + WFClient.CLIENT_VERSION;
            lblNewVer.Text = "Update to: " + info.Update_string;

            txtChangelog.Text = info.Changelog;

            this.info = info;
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (rbWait.Checked)
            {
                this.Close();
            }
            else // Download Update
            {
                System.Diagnostics.Process.Start(info.Download_url);
                Application.Exit();
            }
        }
    }
}
