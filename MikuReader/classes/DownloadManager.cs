using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader
{
    public class DownloadManager
    {
        public readonly FrmStartPage startPage;

        private Stack downloadQueue;
        private ArrayList downloads;
        private int downloadCount;

        public DownloadManager(FrmStartPage startPage)
        {
            this.startPage = startPage;
            downloadQueue = new Stack();
            downloads = new ArrayList();
            downloadCount = 0;
        }

        /// <summary>
        /// Creates a MangaDex download container
        /// </summary>
        /// <param name="chapterNumber">Chapter Number, e.g 7</param>
        /// <param name="chapterID">Chapter ID, e.g 98634</param>
        /// <param name="m">Manga associated with this entry</param>
        /// <param name="downloadUrl">Where to download from</param>
        /// <returns>A Download container</returns>
        private Download CreateMDDownload(string chapterNumber, string chapterID, Manga m, string downloadUrl)
        {
            return new Download(chapterNumber, chapterID, m, downloadUrl, DownloadType.MANGA, this);
        }

        /// <summary>
        /// Creates a nHentai download container
        /// </summary>
        /// <param name="chapterID">Chapter ID, e.g 843254</param>
        /// <param name="m">Manga associated with this entry</param>
        /// <param name="downloadUrl">Where to download from</param>
        /// <returns>A Download container</returns>
        private Download CreateNHDownload(string chapterID, Manga m, string downloadUrl)
        {
            return new Download("1", chapterID, m, downloadUrl, DownloadType.HENTAI, this);
        }

        /// <summary>
        /// Add a MangaDex entry to the download queue
        /// </summary>
        /// <param name="m">Manga associated with this entry</param>
        /// <param name="chapterIDs">List of all chapter IDs to be downloaded</param>
        /// <param name="isUpdate">Is this an update</param>
        public void AddMDToQueue(Manga m, dynamic chapterIDs, bool isUpdate)
        {
            foreach (var chapterID in chapterIDs)
            {
                foreach (var chapterNum in chapterID)
                {
                    if (chapterNum.lang_code == m.settings.lang && // Correct language
                        (m.settings.group == "{Any}" || chapterNum.group_name == m.settings.group) && // Correct group
                        !IsQueueDuplicate(m, (string)chapterNum.chapter)) // Not a dupe chapter
                    {
                        if ((isUpdate && !Directory.Exists(m.mangaDirectory.FullName + "\\" + chapterNum.chapter)) || // If "update" only add if the folder doesn't exist
                            !isUpdate)
                        {
                            string dlUrl = "https://mangadex.org/chapter/" + chapterID.Name + "/1";
                            Download dl = CreateMDDownload((string)chapterNum.chapter, chapterID.Name, m, dlUrl);

                            Logger.Log("Adding MD to queue '" + m.name  + " chapter " + dl.chapterNumber + "'");
                            downloadQueue.Push(dl);
                            downloads.Add(dl);
                            downloadCount++;
                        }
                    }
                }
            }
            DownloadNextFromQueue();
        }

        /// <summary>
        /// Add a nHentai entry to the download queue
        /// </summary>
        /// <param name="m">Manga associated with this entry</param>
        /// <param name="chapterID">Chapter ID, e.g 843254</param>
        public void AddNHToQueue(Manga m, string chapterID)
        {
            string dlUrl = "https://nhentai.net/g/" + chapterID + "/1";
            Download dl = CreateNHDownload(chapterID, m, dlUrl);
            Logger.Log("Adding NH to queue '" + m.name + " chapter " + dl.chapterNumber + "'");
            downloadQueue.Push(dl);
            downloads.Add(dl);
            downloadCount++;

            DownloadNextFromQueue();
        }

        /// <summary>
        /// Determine if the chapter in question already exists in the queue
        /// </summary>
        /// <param name="m">Manga associated with this entry</param>
        /// <param name="chapterNum">The chapter number, e.g 7</param>
        /// <returns>True if the chapter exists in the queue already</returns>
        private bool IsQueueDuplicate(Manga m, string chapterNum)
        {
            foreach (Download d in downloadQueue)
            {
                if (d.manga.name == m.name && d.chapterNumber == chapterNum) return true;
                else return false;
            }
            return false;
        }

        /// <summary>
        /// Get the next Download from the queue and start it
        /// </summary>
        /// <returns>True when done</returns>
        public bool DownloadNextFromQueue()
        {
            if (downloadQueue.Count > 0)
            {
                Download next = (Download)downloadQueue.Pop();
                Logger.Log("Downloading next item from queue '" + next.manga.name + " chapter " + next.chapterNumber + "'");                next.StartDownloading();
                startPage.StartDownloadGUI();
                return false;
            }
            else
            {
                foreach (Download d in downloads)
                {
                    Logger.Log("Deleting '" + d.manga.MangaDirectory + "\\downloading" + "'");
                    if (File.Exists(d.manga.MangaDirectory + "\\downloading"))
                        File.Delete(d.manga.MangaDirectory + "\\downloading");
                }

                startPage.StopDownloadGUI();
                downloadQueue.Clear();
                downloads.Clear();
                downloadCount = 0;
                return true;
            }
        }

        /// <summary>
        /// Get the overall download progress
        /// </summary>
        public void UpdateOverallDownloadProgress()
        {
            float total = 0f;
            foreach(Download dl in downloads)
            {
                float indv = dl.GetProgress();
                float multiplier = (float)downloadCount / (downloadQueue.Count + 1);
                float scaled = indv * multiplier;
                total += scaled;
            }
            if (total > 100f)
                total = 100f;
            startPage.UpdateProgress((int)total);
        }
    }
}
