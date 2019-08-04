﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader
{
    public class Download
    {
        public dynamic num;
        public string chapterID;
        public string mangaDirectory;
        private readonly string url;
        private FrmStartPage startPage;
        private WebBrowser browser;
        private ArrayList clients;
        
        public Download(dynamic num, string chapterID, string mangaDirectory, string url, FrmStartPage startPage)
        {
            this.num = num;
            this.chapterID = chapterID;
            this.mangaDirectory = mangaDirectory;
            this.url = url;
            this.startPage = startPage;
            browser = new WebBrowser();
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentCompletedCallback);
            clients = new ArrayList();
        }

        public void StartDownloading()
        {
            browser.Navigate(url);
        }

        private bool ContinueDownloading()
        {
            string chapterDirectory = mangaDirectory + "\\" + num;
            Directory.CreateDirectory(chapterDirectory); // Create a directory for this chapter

            if (!GetHtml().Contains("#legacy_reader_settings_modal"))
            {
                File.Delete(mangaDirectory + "\\manga.json");
                File.Delete(mangaDirectory + "\\tracker");
                File.Delete(mangaDirectory + "\\downloading");
                Directory.Delete(mangaDirectory, true);
                startPage.DownloadFailedNoLegacy();
                return false;
            }

            string hash = GetChapterHash(GetFullImageUrl(GetHtml()));
            string[] files = GetFilenames(GetHtml());

            for (int i = 0; i < files.Length; i++)
            {
                string page = files[i];
                string imgUrl;
                if (!hash.StartsWith("/data/"))
                {
                    imgUrl = "https://s5.mangadex.org/data/" + hash + "/" + page;
                } else
                {
                    imgUrl = "https://mangadex.org" + hash + "/" + page;
                }
                string imgFile = chapterDirectory + "\\" + page;
                if (File.Exists(imgFile))
                {
                    long length = new FileInfo(imgFile).Length;
                    if (length <= 0)
                    {
                        File.Delete(imgFile);
                        DlAsync(new Uri(imgUrl), imgFile);
                    } else
                    {
                        if (i == files.Length - 1) // last one
                            CheckStartNext();
                    }
                    
                } else
                {
                    DlAsync(new Uri(imgUrl), imgFile);
                }                
            }
            return true;
        }

        private void DlAsync(Uri imgUrl, string imgFile)
        {
            using (WebClient wc = new WebClient())
            {
                clients.Add(wc.ToString());
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompletedCallback);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadOngoingCallback);
                wc.DownloadFileAsync(imgUrl, imgFile);
            }
        }

        private string GetFullImageUrl(string html)
        {
            if (html.Contains("id=\"current_page\""))
            {
                int startpos = html.IndexOf("current_page") + "current_page".Length + 30;
                string sh = html.Substring(startpos, 107);

                int startpos2 = sh.IndexOf("src") + "src".Length;
                int length = 85;
                string imgUrl = sh.Substring(startpos2, length);
                int end = imgUrl.IndexOf("alt") - 2;
                return imgUrl.Substring(0, end);  // example https://s5.mangadex.org/data/7d188d28aaa23214de8957d6f80d1841/x1.png
            }
            else if (html.Contains("class=\"webtoon click\""))
            {
                int startpos = html.IndexOf("webtoon click") + "webtoon click".Length + 7;
                string sh = html.Substring(startpos, 107);

                // int startpos2 = sh.IndexOf("alt") + "alt".Length;
                int length = 85;
                string imgUrl = sh.Substring(0, length);
                int end = imgUrl.IndexOf("alt") - 2;
                return imgUrl.Substring(0, end);  // example https://s5.mangadex.org/data/7d188d28aaa23214de8957d6f80d1841/x1.png
            } else
            {
                MessageBox.Show("Unknown image hosting format found.  Please report this to 9volt for fixing.");
                throw new FormatException();
            }
        }

        private string GetChapterHash(string fullImgUrl)
        {
            if (fullImgUrl.StartsWith("https://"))
            {
                return fullImgUrl.Substring("https://s5.mangadex.org/data/".Length,
                    fullImgUrl.LastIndexOf("/") - "https://s5.mangadex.org/data/".Length);
            } else if (fullImgUrl.StartsWith("/data/"))
            {
                return fullImgUrl.Substring(0, fullImgUrl.LastIndexOf("/"));
                //return fullImgUrl.Substring("/data/".Length,
                    //fullImgUrl.LastIndexOf("/") - "/data/".Length);
            } else
            {
                // ??
                MessageBox.Show("Unknown URL path discovered:\n" + fullImgUrl + "\n\nPlease report this to 9volt so it can be fixed.");
                return String.Empty;
            }
        }

        private string[] GetFilenames(string html)
        {
            int startpos2 = html.IndexOf("page_array") + "page_array".Length + 7;
            string arrayStr = html.Substring(startpos2, html.Length - html.LastIndexOf("]"));
            arrayStr = arrayStr.Substring(0, arrayStr.IndexOf("]") - 1);
            arrayStr = arrayStr.Replace("'", "");
            return arrayStr.Split(',');
        }

        private string GetHtml()
        {
            return browser.DocumentText;
        }

        private void CheckStartNext()
        {
            if (clients.Count == 0)
            {
                startPage.DownloadNextFromQueue(mangaDirectory);
            }
        }

        private void DownloadOngoingCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            
        }

        private void DownloadCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            clients.Remove(sender.ToString());
            CheckStartNext();
        }

        private void DocumentCompletedCallback(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ContinueDownloading();
        }
    }
}
