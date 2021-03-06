﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Implementation of a Download for Mangadex
    /// </summary>
    public class MangaDexDownload : IDownload
    {
        private Chapter chapter;
        private ArrayList clients;
        private float total;

        public event DownloadCompletedEventHandler DownloadCompleted;

        /// <summary>
        /// Create a new Download
        /// </summary>
        /// <param name="chapter">Chapter to download</param>
        public MangaDexDownload(Chapter chapter)
        {
            this.chapter = chapter;
            clients = new ArrayList();
            total = 0;
        }

        public void StartDownloading()
        {
            File.Create(Path.Combine(chapter.GetChapterRoot().Parent.FullName, "dl" + chapter.GetID())).Close();
            string jsonUrl = MangaDexHelper.MANGADEX_URL + "/api/chapter/" + chapter.GetID();
            string jsonString;

            using (var wc = new WebClient())
            {
                jsonString = wc.DownloadString(jsonUrl);
            }

            JObject jobj = JObject.Parse(jsonString);

            string server = (string)jobj["server"];
            string hash = (string)jobj["hash"];

            // string[] page_array = /* ((string) */ jobj["page_array"]. /* ).Split(',') */;
            List<string> page_array = new List<string>();
            IJEnumerable<JToken> jtokens = jobj["page_array"].Values();
            foreach(JToken t in jtokens)
            {
                page_array.Add((string)t);
            }

            foreach (string file in page_array)
            {
                if (server == "/data/")
                    server = MangaDexHelper.MANGADEX_URL + "/data/";

                string imgUrl = server + hash + "/" + file;

                FileInfo imgFile = new FileInfo(
                    Path.Combine(
                        chapter.GetChapterRoot().FullName, 
                        ConvertToNumericFileName(file)
                ));

                if (File.Exists(imgFile.FullName))
                    if (imgFile.Length <= 0)
                        File.Delete(imgFile.FullName);

                DownloadAsync(new Uri(imgUrl), imgFile.FullName);
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

        private string ConvertToNumericFileName(string input)
        {
            string[] parts = input.Split('.'); // x34.png -> {x34 , png}
            string name = Regex.Replace(parts[0], "[^0-9.]", "");
            return name + "." + input[1];  // -> 34.png
        }
    }
}
