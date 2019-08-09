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
    public partial class FrmHentaiBrowser : Form
    {
        private FrmStartPage startPage;

        private GeckoWebBrowser browser;

        public FrmHentaiBrowser(FrmStartPage startPage)
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
            browser.Navigate("https://nhentai.net");
            browser.DocumentCompleted += Browser_DocumentCompleted;
        }

        private void GoBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.GoBack();
        }

        /// <summary>
        /// Checks if the URL is valid and calls for the hentai to be added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddThisTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browser.Url != null)
            {
                string api = browser.Url.ToString();
                string num = string.Empty;
                if (api.StartsWith("https://nhentai.net/g/"))
                {
                    num = api.Split('/')[4];
                    startPage.AddHentai(num, GetTitle());
                }
                else
                {
                    MessageBox.Show("Error: Not a valid nHentai URL");
                }
            }
        }

        private string GetTitle()
        {
            string fullTitle = browser.DocumentTitle;
            return fullTitle.Substring(0, fullTitle.IndexOf("nhentai:") - 3);
        }

        private void Browser_DocumentCompleted(object sender, GeckoDocumentCompletedEventArgs e)
        {
            if (browser.Url != null)
            {
                string url = browser.Url.ToString();
                if (url.StartsWith("https://nhentai.net/g/"))
                {
                    addThisTitleToolStripMenuItem.Enabled = true;
                }
                else
                {
                    addThisTitleToolStripMenuItem.Enabled = false;
                }
            }
        }
    }
}
