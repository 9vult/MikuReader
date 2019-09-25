using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Gecko;
using Gecko.Events;

namespace MikuReader
{
    /// <summary>
    /// Webbrowser for finding new mangas
    /// </summary>
    public partial class FrmMangaBrowser : Form
    {
        FrmStartPage startPage;

        private GeckoWebBrowser browser;

        public FrmMangaBrowser(FrmStartPage startPage)
        {
            InitializeComponent();
            InitializeBrowser();
            this.startPage = startPage;
        }

        private void InitializeBrowser()
        {

            browser = new GeckoWebBrowser();
            browser.Dock = DockStyle.Fill;
            this.browserPanel.Controls.Add(browser);
            Xpcom.Initialize("Firefox");
            browser.Navigate("https://mangadex.org");
            browser.DocumentCompleted += Browser_DocumentCompleted;
        }

        private void GoBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                browser.GoBack();
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Checks if the URL is valid and calls for the manga to be added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddThisTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browser.Url != null)
            {
                string api = browser.Url.ToString();
                string name = string.Empty;
                string num = string.Empty;
                if (api.StartsWith("https://mangadex.org/title/"))
                {
                    name = api.Split('/')[5];
                    num = api.Split('/')[4];
                    api = "https://mangadex.org/api/manga/" + num;
                    startPage.AddManga(api, num, false);
                }
                else
                {
                    MessageBox.Show("Error: Not a valid MangaDex URL");
                }
            }
        }

        private void Browser_DocumentCompleted(object sender, GeckoDocumentCompletedEventArgs e)
        {
            if (browser.Url != null)
            {
                string url = browser.Url.ToString();
                if (url.StartsWith("https://mangadex.org/title/"))
                {
                    addThisTitleToolStripMenuItem.Enabled = true;
                }
                else
                {
                    addThisTitleToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void FrmMangaBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            browser.Dispose();
        }
    }
}
