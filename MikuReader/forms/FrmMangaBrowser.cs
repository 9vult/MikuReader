using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader
{
    /// <summary>
    /// Webbrowser for finding new mangas
    /// </summary>
    public partial class FrmMangaBrowser : Form
    {
        FrmStartPage startPage;

        public FrmMangaBrowser(FrmStartPage startPage)
        {
            InitializeComponent();
            this.startPage = startPage;
        }

        private void GoBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.GoBack();
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

        /// <summary>
        /// Enables/disables the Add button depending on the URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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
    }
}
