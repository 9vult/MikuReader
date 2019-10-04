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
    public partial class FrmLauncher : Form
    {
        public FrmLauncher()
        {
            InitializeComponent();
        }

        private DatabaseManager dbm;

        private void FrmLauncher_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["approot"].ToString() == String.Empty)
            {
                Properties.Settings.Default["approot"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MikuReader2");
            }
            FileHelper.APP_ROOT = FileHelper.CreateDI(Properties.Settings.Default["approot"].ToString());

            this.dbm = new DatabaseManager();

            RepopulateItems();
        }


        private void RepopulateItems()
        {
            lstManga.Items.Clear();
            dbm.Refresh();
            foreach (Manga m in dbm.GetMangaPopulation())
            {
                string shortName = m.GetUserTitle();
                if (shortName.Length > 30)
                {
                    shortName = shortName.Substring(0, 30);
                }
                else
                {
                    shortName = shortName.PadRight(30);
                }
                lstManga.Items.Add(shortName + " » c" + m.GetCurrentChapter() + ",p" + m.GetCurrentPage());
            }

            if (lstManga.Items.Count > 0)
                lstManga.SelectedIndex = 0;
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            string selectedText = lstManga.SelectedItem.ToString();
            string name = selectedText.Substring(0, selectedText.LastIndexOf('»')).Trim();

            Manga selected = null;
            foreach (Manga m in dbm.GetMangaPopulation())
            {
                if (m.GetUserTitle().Equals(name))
                {
                    selected = m;
                    break;
                }
            }

            if (selected != null)
                new FrmSinglePageReader(selected).Show();
            else
                MessageBox.Show("Could not find the specified manga");
        }
    }
}
