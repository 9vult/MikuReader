using Gecko;
using Gecko.Events;
using MikuReader.Core;
using Newtonsoft.Json.Linq;
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
    public partial class FrmBrowser : Form
    {
        public FrmBrowser()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            gfxBrowser.DocumentCompleted += Browser_DocumentCompleted;
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {
            cmboSource.SelectedIndex = 0;
        }

        private void CmboSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmboSource.SelectedItem.ToString().ToLower())
            {
                case "mangadex":
                    gfxBrowser.Navigate("https://mangadex.org");
                    break;
                case "nhentai":
                    gfxBrowser.Navigate("https://nhentai.net");
                    break;
                default:
                    break;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            try { gfxBrowser.GoBack(); } catch (Exception) { }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try { gfxBrowser.Refresh(); } catch (Exception) { }
        }

        private void Browser_DocumentCompleted(object sender, GeckoDocumentCompletedEventArgs e)
        {
            if (gfxBrowser.Url == null) return;
            
            switch (cmboSource.SelectedItem.ToString().ToLower())
            {
                case "mangadex":
                    if (gfxBrowser.Url.ToString().StartsWith("https://mangadex.org/title/"))
                        btnAdd.Enabled = true;
                    else
                        btnAdd.Enabled = false;
                    break;
                case "nhentai":
                    if (gfxBrowser.Url.ToString().StartsWith("https://nhentai.net/g/"))
                        btnAdd.Enabled = true;
                    else
                        btnAdd.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string url = gfxBrowser.Url.ToString();
            string name = string.Empty;
            string num = string.Empty;
            switch (cmboSource.SelectedItem.ToString().ToLower())
            {
                case "mangadex":
                    name = url.Split('/')[5];
                    num = url.Split('/')[4];
                    url = "https://mangadex.org/api/manga/" + num;

                    Manga m = new Manga(FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, num)), url);

                    WFClient.dbm.GetMangaDB().Add(m);
                
                    foreach (Chapter c in m.GetChapters())
                    {
                        WFClient.dlm.AddToQueue(new MangaDexDownload(c));
                    }
                    // Start downloading the first one
                    WFClient.dlm.DownloadNext();
                    break;
                case "nhentai": 
                    num = url.Split('/')[4];
                    name = gfxBrowser.DocumentTitle.Substring(0, gfxBrowser.DocumentTitle.IndexOf("nhentai:") - 3);

                    JObject hJson = new JObject(
                        new JProperty("hentai",
                            new JObject(
                                new JProperty("title", name),
                                new JProperty("num",  num),
                                new JProperty("url",  url))));

                    DirectoryInfo hDir = FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, "h" + num));

                    Hentai h = new Hentai(hDir, hJson.ToString());
                    WFClient.dbm.GetMangaDB().Add(h);

                    Chapter ch = h.GetChapters()[0];
                    WFClient.dlm.AddToQueue(new NhentaiDownload(ch));
                    // Start downloading the first one
                    WFClient.dlm.DownloadNext();
                    break;
            }
            MessageBox.Show("Download started! You may close the browser at any time, but please keep MikuReader open until the download has completed.");
        }
    }
}
