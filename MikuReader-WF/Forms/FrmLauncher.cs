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
    public partial class FrmLauncher : Form
    {
        public FrmLauncher()
        {
            InitializeComponent();
        }

        private void FrmLauncher_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["approot"].ToString() == String.Empty)
            {
                Properties.Settings.Default["approot"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MikuReader2");
                Properties.Settings.Default.Save();
            }
            FileHelper.APP_ROOT = FileHelper.CreateDI(Properties.Settings.Default["approot"].ToString());

            if (File.Exists(Path.Combine(FileHelper.APP_ROOT.FullName, "mikureader.txt")))
            {
                SettingsHelper.Initialize();
            } else
            {
                SettingsHelper.Create();
                SettingsHelper.Initialize();
            }

            WFClient.dlm.ProgressUpdated += new ProgressUpdatedEventHandler(ProgressUpdatedCallback);

            RepopulateItems();
        }

        private void RepopulateItems()
        {
            lstManga.Items.Clear();
            lstHentai.Items.Clear();

            WFClient.dbm.Refresh();
            foreach (Manga m in WFClient.dbm.GetMangaPopulation())
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

            foreach (Hentai h in WFClient.dbm.GetHentaiPopulation())
            {
                string shortName = h.GetUserTitle();
                if (shortName.Length > 30)
                {
                    shortName = shortName.Substring(0, 30);
                }
                else
                {
                    shortName = shortName.PadRight(30);
                }
                lstHentai.Items.Add(shortName + " » p" + h.GetCurrentPage());
            }

            if (lstManga.Items.Count > 0)
                lstManga.SelectedIndex = 0;
            if (lstHentai.Items.Count > 0)
                lstHentai.SelectedIndex = 0;
        }

        private void UpdateMangas()
        {
            List<Chapter> updates = new List<Chapter>();
            foreach (Manga m in WFClient.dbm.GetMangaPopulation())
            {
                Chapter[] u = m.GetUpdates();
                updates.AddRange(u);
            }

            foreach (Chapter c in updates)
            {
                WFClient.dlm.AddToQueue(new MangaDexDownload(c));
            }
            if (updates.Count > 0)
            {
                MessageBox.Show("Downloading " + updates.Count + " new chapters...");
                WFClient.dlm.DownloadNext();
            } else
            {
                MessageBox.Show("All up to date!");
            }
        }

        private void ProgressUpdatedCallback(object sender)
        {
            double percent = WFClient.dlm.GetCompletionPercentage();
            Console.WriteLine(percent + " | " + (int)percent);

            progressBar1.Value = (int)percent;
            if (percent == 100)
            {
                progressBar1.Value = 0;
                RepopulateItems();
            }
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            new FrmSettings().ShowDialog();
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            Title selected = null;
            if (tabControl1.SelectedTab.Text.ToLower() == "mangadex")
            {
                string selectedText = lstManga.SelectedItem.ToString();
                string name = selectedText.Substring(0, selectedText.LastIndexOf('»')).Trim();

                foreach (Manga m in WFClient.dbm.GetMangaPopulation())
                {
                    if (m.GetUserTitle().StartsWith(name))
                    {
                        selected = m;
                        break;
                    }
                }
            }
            else // Hentai
            {
                string selectedText = lstHentai.SelectedItem.ToString();
                string name = selectedText.Substring(0, selectedText.LastIndexOf('»')).Trim();

                foreach (Hentai h in WFClient.dbm.GetHentaiPopulation())
                {
                    if (h.GetUserTitle().StartsWith(name))
                    {
                        selected = h;
                        break;
                    }
                }
            }


            if (selected != null)
            {
                if (selected.IsDownloading())
                {
                    MessageBox.Show("Please wait for this title to finish downloading.");
                    return;
                }

                if (SettingsHelper.UseDoubleReader)
                    new FrmDoublePageReader(selected).Show();
                else
                    new FrmSinglePageReader(selected).Show();
            }
            else
                MessageBox.Show("Could not find the specified title");

        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            new FrmBrowser().Show();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RepopulateItems();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateMangas();
        }
    }
}
