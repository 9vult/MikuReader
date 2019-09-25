using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
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
        public dynamic chapterNumber;
        public string chapterID;
        public Manga manga;
        public DownloadType type;

        private readonly string url;
        private DownloadManager downloadManager;
        private ArrayList clients;
        private int dlCount;

        public Download(string chapterNumber, string chapterID, Manga manga, string url, DownloadType type, DownloadManager downloadManager)
        {
            this.chapterNumber = chapterNumber;
            this.chapterID = chapterID;
            this.manga = manga;
            this.url = url;
            this.type = type;
            this.downloadManager = downloadManager;
            clients = new ArrayList();
            dlCount = 0;
        }

        /// <summary>
        /// Start downloading
        /// </summary>
        /// <returns>True if successful</returns>
        public bool StartDownloading()
        {
            Logger.Log("[DL] Starting Download for '" + manga.mangaDirectory.FullName + "\\" + chapterNumber + "'");
            string chapterDirectory = manga.mangaDirectory.FullName + "\\" + chapterNumber;
            Directory.CreateDirectory(chapterDirectory); // Create a directory for this chapter

            switch (type)
            {
                case DownloadType.MANGA:
                    string jsonUrl = "https://mangadex.org/api/chapter/" + chapterID;
                    string json;
                    using (var wc = new System.Net.WebClient())
                        json = wc.DownloadString(jsonUrl);

                    dynamic contents = JsonConvert.DeserializeObject(json);
                    string server = contents.server;
                    string hash = contents.hash;
                    foreach (string file in contents.page_array)
                    {
                        if (server == "/data/")
                        {
                            server = "https://mangadex.org/data/";
                        }
                        string imgUrl = server + hash + "/" + file;
                        string imgFile = chapterDirectory + "\\" + file;

                        if (File.Exists(imgFile))
                        {
                            long length = new FileInfo(imgFile).Length;
                            if (length <= 0)
                            {
                                File.Delete(imgFile);
                            }
                        }
                        try
                        {
                            DownloadAsync(new Uri(imgUrl), imgFile);
                        } catch (Exception ex)
                        {
                            Logger.Error("[DL] Failed to download '" + imgUrl + "' because '" + ex.Message + "'");
                        }
                    }
                    CheckStartNext();
                    return true;

                case DownloadType.HENTAI:
                    string imageUrl = string.Empty;
                    string extension;
                    int pageCount = 0;
                    imageUrl = NHGetImageUrl(url);
                    pageCount = NHGetPageCount(url);
                    extension = NHGetImageExtension(imageUrl);
                    string imgLoc = NHGetImageLocation(imageUrl);

                    for (int page = 1; page <= pageCount; page++)
                    {
                        dlCount++;
                        string curImgUrl = imgLoc + page + "." + extension;
                        string imgFile = chapterDirectory + "\\" + page + "." + extension;
                        if (File.Exists(imgFile))
                        {
                            long length = new FileInfo(imgFile).Length;
                            if (length <= 0)
                            {
                                File.Delete(imgFile);
                                DownloadAsync(new Uri(curImgUrl), imgFile);
                            }
                            else
                            {
                                if (page == pageCount) // last one
                                    CheckStartNext();
                            }
                        }
                        else
                            DownloadAsync(new Uri(curImgUrl), imgFile);
                    }
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Start an asynch download process
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="imgFile"></param>
        private void DownloadAsync(Uri imgUrl, string imgFile)
        {
            using (WebClient wc = new WebClient())
            {
                Logger.Log("[DL] Downloading '" + imgUrl + "' to '" + imgFile + "'");
                clients.Add(wc);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompletedCallback);
                wc.DownloadFileAsync(imgUrl, imgFile);
            }
        }

        /// <summary>
        /// Starts the next Download if this one is done
        /// </summary>
        private void CheckStartNext()
        {
            if (clients.Count == 0)
            {
                downloadManager.DownloadNextFromQueue();
            }
        }

        /// <summary>
        /// Called by the WebClient when download finishes
        /// </summary>
        private void DownloadCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            clients.Remove((WebClient)sender);
            CheckStartNext();
            downloadManager.UpdateOverallDownloadProgress();
        }

        /// <summary>
        /// Get the current process
        /// </summary>
        /// <returns>The current progress</returns>
        public int GetProgress()
        {
            float c = (float)dlCount / (float)clients.Count;
            return (int)(c);
        }

        #region NHENTAI

        /// <summary>
        /// Get the full image URL for the specified page URL
        /// </summary>
        /// <param name="url">Page URl to get the image from</param>
        /// <returns>The full image URL</returns>
        private string NHGetImageUrl(string url)
        {
            var document = new HtmlWeb().Load(url);

            string[] urls = document.DocumentNode.Descendants("img")
                .Select(e => e.GetAttributeValue("src", null))
                .Where(s => !String.IsNullOrEmpty(s)).ToArray();

            foreach (string imgurl in urls)
            {
                if (imgurl.StartsWith("https://i.nhentai.net/galleries"))
                    return imgurl;
            }
            MessageBox.Show("Could not find image url for\n\"" + url + "\"\n\nPlease report this to 9volt.");
            return null;
            // var document = new HtmlWeb().Load(url);
            // return document.DocumentNode.SelectSingleNode("/html/body/div[2]/div/section[2]/a/img").InnerText;
        }

        /// <summary>
        /// Gets the first part of the image URL
        /// </summary>
        /// <param name="imageUrl">Full image URL</param>
        /// <returns>The first part of the image URL</returns>
        private string NHGetImageLocation(string imageUrl)
        {
            return imageUrl.Substring(0, imageUrl.Length - 5);
        }

        /// <summary>
        /// Gets the number of pages in the chapter
        /// </summary>
        /// <param name="url">Page URl to get the count from</param>
        /// <returns>The page count</returns>
        private int NHGetPageCount(string url)
        {
            HtmlAgilityPack.HtmlDocument document;
            if (url.EndsWith("/"))
            {
                document = new HtmlWeb().Load(url.Substring(0, url.Length - 2));
            }
            else
            {
                document = new HtmlWeb().Load(url.Substring(0, url.Length - 1));
            }

            int startIndex = document.DocumentNode.InnerHtml.IndexOf("\"num_pages\":") + 12;
            int length = document.DocumentNode.InnerHtml.IndexOf(",\"num_favorites\"") - startIndex;
            return int.Parse(document.DocumentNode.InnerHtml.Substring(startIndex, length));
        }

        /// <summary>
        /// Gets the filename extension for this chapter
        /// </summary>
        /// <param name="imageUrl">Image URL to get the extension from</param>
        /// <returns>The image filename extension</returns>
        private string NHGetImageExtension(string imageUrl)
        {
            string imgLoc = NHGetImageLocation(imageUrl);
            return imageUrl.Substring(imgLoc.Length, imageUrl.Length - imgLoc.Length).Split('.')[1];
        }

        #endregion NHENTAI
    }
}
