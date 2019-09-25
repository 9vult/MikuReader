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
    public partial class FrmHentaiSettings : Form
    {
        private Manga m;

        public FrmHentaiSettings(Manga m)
        {
            this.m = m;
            InitializeComponent();
        }

        private void FrmHentaiSettings_Load(object sender, EventArgs e)
        {
            if (File.Exists(m.mangaDirectory.FullName + "\\title"))
            {
                txtName.Text = File.ReadAllText(m.mangaDirectory.FullName + "\\title");
            }
        }

        private void FrmHentaiSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (txtName.Text != "")
            {
                File.WriteAllText(m.mangaDirectory.FullName + "\\title", txtName.Text);
            }
            DialogResult = DialogResult.OK;
        }

        private void FrmHentaiSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }
    }
}
