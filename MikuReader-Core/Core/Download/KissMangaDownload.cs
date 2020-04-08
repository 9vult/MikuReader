using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace MikuReader.Core
{
    public class KissMangaDownload : IDownload
    {
        private Chapter chapter;
        private ArrayList clients;
        private float total;

        public event DownloadCompletedEventHandler DownloadCompleted;

        public KissMangaDownload(Chapter chapter)
        {
            this.chapter = chapter;
            clients = new ArrayList();
            total = 0;
        }

        public void StartDownloading()
        {
            File.Create(Path.Combine(chapter.GetChapterRoot().Parent.FullName, "dl" + chapter.GetID())).Close();

            string mName = chapter.GetChapterRoot().Parent.Name;
            string cUrl = "https://kissmanga.org/chapter/" + mName + "/" + chapter.GetID();
            string[] pageUrls = KissMangaHelper.GetPageUrls(cUrl);        
            
            foreach (string url in pageUrls)
            {
                string imgFile = Path.Combine(chapter.GetChapterRoot().FullName, KissMangaHelper.GetPageFileName(url));
                DownloadAsync(new Uri(url), imgFile);
            }
        }

        public void DownloadAsync(Uri imgUrl, string imgFile)
        {
            using (WebClient wc = new WebClient())
            {
                total++;
                clients.Add(wc);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompletedCallback);
                wc.DownloadFileAsync(imgUrl, imgFile);
            }
        }

        public bool CheckStartNext()
        {
            if (clients.Count == 0)
            {
                DownloadCompleted.Invoke(this);
                File.Delete(Path.Combine(chapter.GetChapterRoot().Parent.FullName, "dl" + chapter.GetID()));
                return true;
            }
            return false;
        }

        public void DownloadCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            clients.Remove((WebClient)sender);
            CheckStartNext();
        }

        public int GetProgress()
        {
            return total != 0 ? (int)(((total - clients.Count) / total) * 100) : 0;
        }

        public string GetChapterName()
        {
            return chapter.GetChapterRoot().ToString();
        }
    }
}
