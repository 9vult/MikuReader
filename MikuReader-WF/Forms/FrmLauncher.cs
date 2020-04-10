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
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader.wf.Forms
{
    public partial class FrmLauncher : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font bigFont;
        Font smallFont;

        public FrmLauncher()
        {
            InitializeComponent();

            byte[] fontData = Properties.Resources.Franchise_Bold_hinted;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Franchise_Bold_hinted.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Franchise_Bold_hinted.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            bigFont = new Font(fonts.Families[0], 36.0F);
            smallFont = new Font(fonts.Families[0], 20.0F);
        }

        private void FrmLauncher_Load(object sender, EventArgs e)
        {
            WFClient.logger.Log("Starting");

            // Set embedded fonts
            label1.Font = bigFont;
            label2.Font = smallFont;


            if (Properties.Settings.Default["approot"].ToString() == String.Empty)
            {
                Properties.Settings.Default["approot"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MikuReader2");
                Properties.Settings.Default.Save();
            }
            FileHelper.APP_ROOT = FileHelper.CreateDI(Properties.Settings.Default["approot"].ToString());
            Directory.CreateDirectory(FileHelper.APP_ROOT.FullName);

            WFClient.logger.Log("Loading from " + FileHelper.APP_ROOT);

            if (File.Exists(Path.Combine(FileHelper.APP_ROOT.FullName, "mikureader.txt")))
            {
                SettingsHelper.Initialize();
            }
            else
            {
                SettingsHelper.Create();
                SettingsHelper.Initialize();
            }

            WFClient.dlm.ProgressUpdated += new ProgressUpdatedEventHandler(ProgressUpdatedCallback);

            RepopulateItems();

            if (SettingsHelper.CheckForUpdates)
                Updater.Start();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            new FrmSettings().ShowDialog();
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            Title selected = GetSelected();
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

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            new FrmEdit(GetSelected()).Show();
        }

        private void ShowAboutBox(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }

        /// <summary>
        /// Get the currently selected Title
        /// </summary>
        /// <returns>The currently selected Title</returns>
        private Title GetSelected()
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
                return selected;
            }
            else
                MessageBox.Show("Could not find the specified title");

            return null;
        }

        /// <summary>
        /// Populate the two listboxes with the Titles from the databases
        /// </summary>
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

        /// <summary>
        /// Check for and start downloading new chapters (MangaDex only)
        /// </summary>
        private void UpdateMangas()
        {
            List<Chapter> updates = new List<Chapter>();
            foreach (Manga m in WFClient.dbm.GetMangaPopulation())
            {
                Chapter[] u = m.GetUpdates();
                updates.AddRange(u);

                if (m is MangaDex)
                {
                    foreach (Chapter c in u)
                    {
                        WFClient.dlm.AddToQueue(new MangaDexDownload(c));
                    }
                }
                else if (m is KissManga)
                {
                    foreach (Chapter c in u)
                    {
                        WFClient.dlm.AddToQueue(new KissMangaDownload(c));
                    }
                } else
                {
                    MessageBox.Show("An error occured while updating manga: Invalid manga type");
                }
            }
            if (updates.Count > 0)
            {
                MessageBox.Show("Downloading " + updates.Count + " new chapters...");
                WFClient.dlm.DownloadNext();
            }
            else
            {
                MessageBox.Show("All up to date!");
            }
        }

        /// <summary>
        /// Updates the progressbar and status label with download information
        /// </summary>
        /// <param name="sender"></param>
        private void ProgressUpdatedCallback(object sender)
        {
            double percent = WFClient.dlm.GetCompletionPercentage();
            Console.WriteLine(percent + " | " + (int)percent);

            progressBar1.Value = (int)percent;
            lblStatus.Text = "Downloading to " + WFClient.dlm.GetDownloadName() + "...";

            if (percent == 100)
            {
                progressBar1.Value = 0;
                lblStatus.Text = "Ready";
                RepopulateItems();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string mUrl = "https://kissmanga.org/manga/fruits_basket";

            string name = KissMangaHelper.GetName(mUrl);
            string[] chapters = KissMangaHelper.GetChapterUrls(mUrl);

            string cUrl = chapters[0];

            string[] pages = KissMangaHelper.GetPageUrls(KissMangaHelper.KISS_URL + cUrl);
            string hash = KissMangaHelper.GetHash(mUrl);
            bool x = false;
        }
    }
}
