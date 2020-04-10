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
        // TODO GeckoFX
        // GeckoWebBrowser gfxBrowser;
        WebBrowser gfxBrowser;

        public FrmBrowser()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

            // TODO : GeckoFX
            /*
                gfxBrowser = new GeckoWebBrowser
                {
                    Dock = DockStyle.Fill
                };
                panel1.Controls.Add(gfxBrowser);

                Xpcom.Initialize("Firefox");
                gfxBrowser.DocumentCompleted += Browser_DocumentCompleted;
            */
            // TODO : Remove (replace with gfx)
            gfxBrowser = new WebBrowser
            {
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true
            };
            panel1.Controls.Add(gfxBrowser);
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
                    gfxBrowser.Navigate(MangaDexHelper.MANGADEX_URL);
                    break;
                case "kissmanga":
                    gfxBrowser.Navigate(KissMangaHelper.KISS_URL);
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

        // TODO : GeckoFx
        // private void Browser_DocumentCompleted(object sender, GeckoDocumentCompletedEventArgs e)
        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (gfxBrowser.Url == null) return;
            
            switch (cmboSource.SelectedItem.ToString().ToLower())
            {
                case "mangadex":
                    if (gfxBrowser.Url.ToString().StartsWith(MangaDexHelper.MANGADEX_URL + "/title/"))
                        btnAdd.Enabled = true;
                    else
                        btnAdd.Enabled = false;
                    break;
                case "kissmanga":
                    if(gfxBrowser.Url.ToString().StartsWith(KissMangaHelper.KISS_URL + "/manga/"))
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
                    url = MangaDexHelper.MANGADEX_URL + "/api/manga/" + num;

                    Manga m = new MangaDex(FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, num)), url);

                    FrmEdit editor = new FrmEdit(m, false);
                    DialogResult result = editor.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // cleanup
                        foreach (Chapter ch in m.GetChapters())
                        {
                            try
                            {
                                string s = ch.GetChapterRoot().ToString();
                                Directory.Delete(ch.GetChapterRoot().ToString());
                            } catch (Exception) { }
                        }

                        WFClient.dbm.GetMangaDB().Add(m);

                        String[] dls = m.GetDLChapters();
                        if (dls == null || dls[0].Equals("-1"))
                        {
                            foreach (Chapter c in m.GetSetPrunedChapters(false))
                            {
                                WFClient.dlm.AddToQueue(new MangaDexDownload(c));
                            }
                        } else
                        {   // Only download selected chapters
                            foreach (Chapter c in m.GetSetPrunedChapters(false))
                            {
                                if (dls.Contains(c.GetNum()))
                                {
                                    WFClient.dlm.AddToQueue(new MangaDexDownload(c));
                                }
                            }
                        }
                        // Start downloading the first one
                        WFClient.dlm.DownloadNext();
                    } else
                    {

                    }
                    break;
                case "kissmanga":

                    // MessageBox.Show("Sorry, can't do this yet! ;(\n\n(ignore the download started box)");
                    // break;
                    // TODO

                    string kName = KissMangaHelper.GetName(url);
                    string kHash = KissMangaHelper.GetHash(url);

                    // Manga km = new Manga(FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, num)), url);

                    Manga km = new KissManga(FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, kHash)), url);

                    FrmEdit editor1 = new FrmEdit(km, false);
                    DialogResult result1 = editor1.ShowDialog();

                    if (result1 == DialogResult.OK)
                    {
                        // cleanup
                        foreach (Chapter ch in km.GetChapters())
                        {
                            try
                            {
                                string s = ch.GetChapterRoot().ToString();
                                Directory.Delete(ch.GetChapterRoot().ToString());
                            }
                            catch (Exception) { }
                        }

                        WFClient.dbm.GetMangaDB().Add(km);

                        String[] dls = km.GetDLChapters();
                        if (dls == null || dls[0].Equals("-1"))
                        {
                            foreach (Chapter c in km.GetSetPrunedChapters(false))
                            {
                                WFClient.dlm.AddToQueue(new KissMangaDownload(c));
                            }
                        }
                        else
                        {   // Only download selected chapters
                            foreach (Chapter c in km.GetSetPrunedChapters(false))
                            {
                                if (dls.Contains(c.GetNum()))
                                {
                                    WFClient.dlm.AddToQueue(new KissMangaDownload(c));
                                }
                            }
                        }
                        // Start downloading the first one
                        WFClient.dlm.DownloadNext();
                    }
                    else
                    {

                    }
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

                    Hentai h = new Nhentai(hDir, hJson.ToString());

                    FrmEdit edit = new FrmEdit(h, false);
                    DialogResult rezult = edit.ShowDialog();

                    if (rezult == DialogResult.OK)
                    {
                        WFClient.dbm.GetMangaDB().Add(h);

                        Chapter ch = h.GetChapters()[0];
                        WFClient.dlm.AddToQueue(new NhentaiDownload(ch));
                        // Start downloading the first one
                        WFClient.dlm.DownloadNext();
                    } 
                    break;
            }
            MessageBox.Show("Download started! You may close the browser at any time, but please keep MikuReader open until the download has completed.");
        }
    }
}
