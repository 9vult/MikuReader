using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Newtonsoft.Json;
using System.Net;
using HtmlAgilityPack;
using System.Threading;
using System.Collections;
using AutoUpdaterDotNET;

namespace MikuReader
{
    public partial class FrmStartPage : Form
    {
        string homeFolder;
        ArrayList mangas = new ArrayList();
        ArrayList wcList = new ArrayList();
        Stack downloadStack = new Stack();
        int downloads = 0;

        public FrmStartPage()
        {
            InitializeComponent();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            new FrmSettings(this).ShowDialog();
            //new FrmAbout().ShowDialog();
        }

        private void FrmStartPage_Load(object sender, EventArgs e)
        {
            // updater
            if ((bool)Properties.Settings.Default["checkForUpdates"] == true)
            {
                AutoUpdater.Start("https://www.dropbox.com/s/8pf1shiotl68fqv/updateinfo.xml?raw=1");
                AutoUpdater.RunUpdateAsAdmin = true;
                AutoUpdater.DownloadPath = homeFolder + "\\update";
            }


            if ((string)Properties.Settings.Default["homeDirectory"] == "")
            {
                homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MikuReader";
                Properties.Settings.Default["homeDirectory"] = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MikuReader";
                Properties.Settings.Default.Save();
            } else
            {
                homeFolder = (string)Properties.Settings.Default["homeDirectory"];
            }

            if (Directory.Exists(homeFolder))
            {
                RefreshContents();
                if (lstNames.Items.Count > 0)
                {
                    lstNames.SelectedIndex = 0;
                }
            }
            else
            {
                Directory.CreateDirectory(homeFolder);
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            new FrmBrowse(this).Show();
        }

        private void BtnViewFolder_Click(object sender, EventArgs e)
        {
            RefreshContents();
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            if (lstNames.Items.Count > 0)
            {
                string selectedName = lstNames.SelectedItem.ToString().Split('|')[0].Trim();
                foreach (Manga m in mangas)
                {
                    if (m.name.StartsWith(selectedName))
                    {
                        if (!File.Exists(m.mangaDirectory.FullName + "\\downloading"))
                        {
                            if ((bool)Properties.Settings.Default["doublePageReader"] == false)
                            {
                                FrmReader reader = new FrmReader();
                                reader.Show();
                                reader.StartUp(m, this);
                            }
                            else
                            {
                                FrmDoublePageReader reader = new FrmDoublePageReader();
                                reader.Show();
                                reader.StartUp(m, this);
                            }
                        } else
                        {
                            MessageBox.Show("Cannot read '" + m.name + "' because it is currently being downloaded");
                        }
                        break;
                    }
                }
            }
        }        

        /// <summary>
        /// Adds a manga
        /// </summary>
        /// <param name="api">API link</param>
        /// <param name="num">Chapter Number</param>
        public void AddManga(string api, string num)
        {
            string json;
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString(api);
            }

            // Deserialize the JSON file
            dynamic contents = JsonConvert.DeserializeObject(json);
            string mangaName = contents.manga.title;
            string mangaDirectory = homeFolder + "\\" + num;

            if (!Directory.Exists(mangaDirectory))
            {
                Directory.CreateDirectory(mangaDirectory);
                File.WriteAllText(mangaDirectory + "\\manga.json", json); // Write the JSON to a file
                File.WriteAllText(mangaDirectory + "\\tracker", "1|1"); // Write initial tracking info to a file
                File.WriteAllText(mangaDirectory + "\\downloading", ""); // Create "Downloading" file
            }

            MessageBox.Show("Downloading data...");
            foreach (var chapterID in contents.chapter)
            {
                foreach (var chapter in chapterID)
                {
                    if (chapter.lang_code == txtLangCode.Text.ToLower() && !DQDuplicate((string)chapter.chapter))
                    {
                        string dlUrl = "https://mangadex.org/chapter/" + chapterID.Name + "/1";
                        Download dc = new Download(chapter.chapter, chapterID.Name, mangaDirectory, dlUrl, this);
                        downloadStack.Push(dc);
                        downloads++;
                    }
                }
            }

            DownloadNextFromQueue(mangaDirectory);

            RefreshContents();
        }

        public void DownloadFailedNoLegacy()
        {
            MessageBox.Show("Could not download because Reader is not set to Legacy.\n" +
                    "To fix, go to MangaDex Settings, and set Reader to Legacy.");
            downloadStack.Clear();
            DownloadFinished();
        }

        private bool DQDuplicate(string num)
        {
            foreach (Download d in downloadStack)
            {
                if (d.num == num) return true;
                else return false;
            }
            return false;
        }

        public void DownloadNextFromQueue(string mangaDirectory)
        {
            if (downloadStack.Count > 0)
            {
                if (!timer1.Enabled) timer1.Start();
                lblDownloading.Hide();
                if (!currentProgress.Visible) currentProgress.Show();
                UpdateDownloadProgress();
                Download next = (Download)downloadStack.Pop();
                next.StartDownloading();
            } else
            {
                DownloadFinished();
                File.Delete(mangaDirectory + "\\downloading");
            }

        }

        private void DownloadFinished()
        {
            if (timer1.Enabled) timer1.Stop();
            currentProgress.Hide();
            lblDownloading.Hide();
            downloads = 0;
            currentProgress.Value = 0;
        }

        public void UpdateDownloadProgress()
        {
            int done = downloads - downloadStack.Count;
            float multiplier = 100.0f / (float) downloads;
            try
            {
                currentProgress.Value = (int) (done * multiplier);
            } catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Refreshes the UI
        /// </summary>
        public void RefreshContents()
        {
            lstNames.Items.Clear();
            mangas.Clear();
            DirectoryInfo root = new DirectoryInfo(homeFolder);
            DirectoryInfo[] dirs = root.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo dir in dirs)
            {
                var json = File.ReadAllText(dir.FullName + "\\manga.json");
                var tracker = File.ReadAllText(dir.FullName + "\\tracker");
                string[] trackerData = tracker.Split('|');
                // Deserialize the JSON file
                dynamic contents = JsonConvert.DeserializeObject(json);
                string mangaName = contents.manga.title;
                Manga m = new Manga(mangaName, dir, trackerData[0], trackerData[1]);
                mangas.Add(m);
                string name = m.name;
                if (name.Length > 21)
                {
                    name = name.Substring(0, 21);
                } else
                {
                    name = name.PadRight(21);
                }
                lstNames.Items.Add(name + "  |  c" + trackerData[0] + ",p" + trackerData[1]);
            }
            if (lstNames.Items.Count > 0)
            {
                lstNames.SelectedIndex = 0;
            }

            txtLangCode.Text = (string)Properties.Settings.Default["languageCode"];
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (lblDownloading.Visible)
                lblDownloading.Hide();
            else
                lblDownloading.Show();
        }
    }
}
