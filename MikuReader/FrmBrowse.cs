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
    public partial class FrmBrowse : Form
    {
        FrmStartPage startPage;

        public FrmBrowse(FrmStartPage startPage)
        {
            InitializeComponent();
            this.startPage = startPage;
        }

        private void GoBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.GoBack();
        }

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
                    startPage.AddManga(api, num);
                }
                else
                {
                    MessageBox.Show("Error: Not a valid MangaDex URL");
                }
            }
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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
