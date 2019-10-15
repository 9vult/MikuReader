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
    public class NhentaiDownload : IDownload
    {
        private Chapter chapter;
        private ArrayList clients;
        private float total;

        public event DownloadCompletedEventHandler DownloadCompleted;

        public NhentaiDownload(Chapter chapter)
        {
            this.chapter = chapter;
            clients = new ArrayList();
            total = 0;
        }

        public void StartDownloading()
        {
            List<HtmlDocument> docs = new List<HtmlDocument>();
            int pageCount = Nhentai.GetPageCount("https://nhentai.net/g/" + chapter.GetID());

            string hUrl = "https://nhentai.net/g/" + chapter.GetID() + "/";

            string baseUrl = Nhentai.GetImageUrl(hUrl + "1");
            string hash = Nhentai.GetHash(baseUrl);
            
            for (int page = 1; page <= pageCount; page++)
            {
                string imgUrl = Nhentai.ImgBase() + hash + "/" + page + "." + Nhentai.GetExt(baseUrl);
                string imgFile = Path.Combine(chapter.GetChapterRoot().FullName, page + "." + Nhentai.GetExt(baseUrl));
                DownloadAsync(new Uri(imgUrl), imgFile);
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

    }
}
