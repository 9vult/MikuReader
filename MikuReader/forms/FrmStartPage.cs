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
    /// <summary>
    /// Start page for MikuReader
    /// </summary>
    public partial class FrmStartPage : Form
    {
        string homeFolder;
        ArrayList mangas = new ArrayList();
        ArrayList wcList = new ArrayList();
        Stack downloadStack = new Stack();
        int downloads = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmStartPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display the Settings dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            new FrmSettings(this).ShowDialog();
            //new FrmAbout().ShowDialog();
        }

        /// <summary>
        /// General setting up, called when the form first loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmStartPage_Load(object sender, EventArgs e)
        {
            if ((string)Properties.Settings.Default["homeDirectory"] == "")
            {
                homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MikuReader";
                Properties.Settings.Default["homeDirectory"] = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MikuReader";
                Properties.Settings.Default.Save();
            }
            else
            {
                homeFolder = (string)Properties.Settings.Default["homeDirectory"];
            }

            if (Directory.Exists(homeFolder))
            {
                RefreshContents();
                if (lstManga.Items.Count > 0)
                {
                    lstManga.SelectedIndex = 0;
                }
            }
            else
            {
                Directory.CreateDirectory(homeFolder);
            }

            // updater
            if ((bool)Properties.Settings.Default["checkForUpdates"] == true)
            {
                AutoUpdater.Start("https://www.dropbox.com/s/8pf1shiotl68fqv/updateinfo.xml?raw=1");
                AutoUpdater.RunUpdateAsAdmin = true;
                AutoUpdater.DownloadPath = homeFolder + "\\update";
            }

        }

        /// <summary>
        /// Display the manga browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseManga_Click(object sender, EventArgs e)
        {
            new FrmMangaBrowser(this).Show();
        }


        private void BtnRefreshManga_Click(object sender, EventArgs e)
        {
            RefreshContents();
        }

        /// <summary>
        /// Opens the reader to the selected manga if it is not currently downloading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadManga_Click(object sender, EventArgs e)
        {
            if (lstManga.Items.Count > 0)
            {
                string selectedName = lstManga.SelectedItem.ToString().Split('»')[0].Trim();
                foreach (Manga m in mangas)
                {
                    if (m.settings.name.StartsWith(selectedName))
                    {
                        read(m);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Opens the reader to the selected hentai if it is not currently downloading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadHentai_Click(object sender, EventArgs e)
        {
            if (lstHentai.Items.Count > 0)
            {
                string selectedName = lstHentai.SelectedItem.ToString().Split('»')[0].Trim();
                foreach (Manga m in mangas)
                {
                    if (m.name.StartsWith(selectedName))
                    {
                        read(m);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles opening the reader
        /// </summary>
        /// <param name="m"></param>
        private void read(Manga m)
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
            }
            else
            {
                MessageBox.Show("Cannot read '" + m.name + "' because it is currently being downloaded");
            }
        }

        /// <summary>
        /// Adds a manga to the database, doubles as a full check of all the files
        /// </summary>
        /// <param name="api">API link</param>
        /// <param name="num">Chapter Number</param>
        public void AddManga(string api, string num, bool update)
        {
            string json;
            if (api != null)
            { 
                using (var wc = new System.Net.WebClient())
                {
                    json = wc.DownloadString(api);
                }
            } else
            {
                json = File.ReadAllText(homeFolder + "\\" + num + "\\manga.json");
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
            }
            File.WriteAllText(mangaDirectory + "\\downloading", ""); // Create "Downloading" file

            Manga m = new Manga(mangaName, new DirectoryInfo(mangaDirectory), "1", "1");

            DialogResult result = new FrmMangaSettings(m).ShowDialog();

            MessageBox.Show("Downloading data...\nYou may close the Browser as you desire.");
            foreach (var chapterID in contents.chapter)
            {
                foreach (var chapter in chapterID)
                {
                    if (chapter.lang_code == m.settings.lang && // Correct language
                        (m.settings.group == "{Any}" || chapter.group_name == m.settings.group) && // Correct group
                        !DSDuplicate((string)chapter.chapter)) // Not a dupe chapter
                    {
                        if ((update && !Directory.Exists(mangaDirectory + "\\" + chapter.chapter)) || // If "update" only add if the folder doesn't exist
                            !update)
                        {
                            string dlUrl = "https://mangadex.org/chapter/" + chapterID.Name + "/1";
                            Download dc = new Download(chapter.chapter, chapterID.Name, mangaDirectory, dlUrl, DownloadType.MANGA, this);
                            downloadStack.Push(dc);
                            downloads++;
                        }
                    }
                }
            }
            DownloadNextFromQueue(mangaDirectory);

            RefreshContents();
        }

        /// <summary>
        /// Add a Hentai to the database
        /// </summary>
        /// <param name="numbers">le numbers</param>
        public void AddHentai(string numbers, string title)
        {
            string hentaiDirectory = homeFolder + "\\h" + numbers;

            if (!Directory.Exists(hentaiDirectory))
            {
                Directory.CreateDirectory(hentaiDirectory);
                File.WriteAllText(hentaiDirectory + "\\tracker", "1|1"); // Write initial tracking info to a file
                File.WriteAllText(hentaiDirectory + "\\title", title); // Write the title to a file
            }
            File.WriteAllText(hentaiDirectory + "\\downloading", ""); // Create "Downloading" file

            Manga m = new Manga("", new DirectoryInfo(hentaiDirectory), "1", "1");
            DialogResult r = new FrmHentaiSettings(m).ShowDialog();

            MessageBox.Show("Downloading data...\nYou may close the Browser as you desire.");
            string dlUrl = "https://nhentai.net/g/" + numbers + "/1";
            Download dc = new Download("1", numbers, hentaiDirectory, dlUrl, DownloadType.HENTAI, this);
            downloadStack.Push(dc);
            downloads++;
            DownloadNextFromQueue(hentaiDirectory);
            RefreshContents();
        }

        /// <summary>
        /// Checks for new chapters and downloads them
        /// </summary>
        private void UpdateAllManga()
        {
            DirectoryInfo root = new DirectoryInfo(homeFolder);
            DirectoryInfo[] mangoDirectories = root.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo mango in mangoDirectories)
            {
                if (mango.Name == "update")
                    continue;
                else if (!mango.Name.StartsWith("h"))
                {
                    AddManga(null, mango.Name, true);
                }
            }

            RefreshContents();
        }

        /// <summary>
        /// Alerts the user if the download could not proceed because they are not in legacy mode
        /// </summary>
        public void MDownloadFailedNoLegacy()
        {
            MessageBox.Show("Could not download because Reader is not set to Legacy.\n" +
                    "To fix, go to MangaDex Settings, and set Reader to Legacy.");
            downloadStack.Clear();
            DownloadFinished();
        }

        /// <summary>
        /// Check if there is already a copy of the chapter number in the stack
        /// </summary>
        /// <param name="num">Chapter Number</param>
        /// <returns></returns>
        private bool DSDuplicate(string num)
        {
            foreach (Download d in downloadStack)
            {
                if (d.num == num) return true;
                else return false;
            }
            return false;
        }

        /// <summary>
        /// Actually a stack, but downloads the next item from the top
        /// </summary>
        /// <param name="directory">Manga Directory</param>
        public void DownloadNextFromQueue(string directory)
        {
            if (downloadStack.Count > 0)
            {
                if (!timer1.Enabled) timer1.Start();
                lblDownloading.Hide();
                if (!currentProgress.Visible) currentProgress.Show();
                UpdateDownloadProgress();
                Download next = (Download)downloadStack.Pop();
                next.StartDownloading();
            }
            else
            {
                DownloadFinished();
                File.Delete(directory + "\\downloading");
            }

        }

        /// <summary>
        /// Called when all downloads are complete
        /// </summary>
        private void DownloadFinished()
        {
            if (timer1.Enabled) timer1.Stop();
            currentProgress.Hide();
            lblDownloading.Hide();
            downloads = 0;
            currentProgress.Value = 0;
        }

        /// <summary>
        /// Updates the download progress bar
        /// </summary>
        public void UpdateDownloadProgress()
        {
            int done = downloads - downloadStack.Count;
            float multiplier = 100.0f / (float)downloads;
            try
            {
                currentProgress.Value = (int)(done * multiplier);
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Refreshes the UI and its contents
        /// </summary>
        public void RefreshContents()
        {
            lstManga.Items.Clear();
            lstHentai.Items.Clear();
            mangas.Clear();
            DirectoryInfo root = new DirectoryInfo(homeFolder);
            DirectoryInfo[] dirs = root.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo dir in dirs)
            {
                if (dir.Name == "update")
                    continue;
                else if (!dir.Name.StartsWith("h"))
                {
                    var json = File.ReadAllText(dir.FullName + "\\manga.json");
                    var tracker = File.ReadAllText(dir.FullName + "\\tracker");
                    string[] trackerData = tracker.Split('|');
                    // Deserialize the JSON file
                    dynamic contents = JsonConvert.DeserializeObject(json);
                    string mangaName = contents.manga.title;
                    Manga m = new Manga(mangaName, dir, trackerData[0], trackerData[1]);
                    m.LoadSettings();
                    if (m.settings.name == "" || m.settings.name == null)
                    {
                        m.SaveSettings(m.settings.lang, m.settings.group, mangaName);
                        m.LoadSettings();
                    }

                    mangas.Add(m);
                    string name = m.settings.name;
                    if (name.Length > 21)
                    {
                        name = name.Substring(0, 21);
                    }
                    else
                    {
                        name = name.PadRight(21);
                    }
                    lstManga.Items.Add(name + "  »  c" + trackerData[0] + ",p" + trackerData[1]);
                }
                else // Hentai
                {
                    string title = File.ReadAllText(dir.FullName + "\\title");
                    var tracker = File.ReadAllText(dir.FullName + "\\tracker");
                    string[] trackerData = tracker.Split('|');

                    Manga m = new Manga(title, dir, trackerData[0], trackerData[1]);
                    mangas.Add(m);
                    string name = m.name;
                    if (name.Length > 21)
                    {
                        name = name.Substring(0, 21);
                    }
                    else
                    {
                        name = name.PadRight(21);
                    }
                    lstHentai.Items.Add(name + "  »  p" + trackerData[1]);
                }
            }
            if (lstManga.Items.Count > 0)
            {
                lstManga.SelectedIndex = 0;
            }
            if (lstHentai.Items.Count > 0)
            {
                lstHentai.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Controls the blinking "Downloading" label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (lblDownloading.Visible)
                lblDownloading.Hide();
            else
                lblDownloading.Show();
        }

        /// <summary>
        /// Opens the Manga Settings dialog for the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnIndMangaSettings_Click(object sender, EventArgs e)
        {
            foreach (Manga m in mangas)
            {
                if (m.settings != null)
                {
                    if (m.settings.name.StartsWith(lstManga.SelectedItem.ToString().Split('»')[0].Trim()))
                    {
                        new FrmMangaSettings(m).ShowDialog();
                        break;
                    }
                }
                
            }
            RefreshContents();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAllManga();
        }

        private void BtnBrowseHentai_Click(object sender, EventArgs e)
        {
            new FrmHentaiBrowser(this).Show();
        }

        private void btnHentaiSettings_Click(object sender, EventArgs e)
        {
            foreach (Manga m in mangas)
            {
                if (File.Exists(m.mangaDirectory.FullName + "\\title"))
                {
                    string title = File.ReadAllText(m.mangaDirectory.FullName + "\\title");
                    if (title.StartsWith(lstHentai.SelectedItem.ToString().Split('»')[0].Trim()))
                    {
                        new FrmHentaiSettings(m).ShowDialog();
                        break;
                    }
                }
            }
            RefreshContents();
        }

        private void btnRefreshHentai_Click(object sender, EventArgs e)
        {
            RefreshContents();
        }
    }
}
