using System;
using System.Collections.Generic;
using System.Text;

namespace MikuReader.Core
{
    /// <summary>
    /// EventHandler for notifying subscribers that the progress has been updated
    /// </summary>
    /// <param name="sender"></param>
    public delegate void ProgressUpdatedEventHandler(object sender);

    /// <summary>
    /// Manages downloads
    /// </summary>
    public class DownloadManager
    {
        private Stack<IDownload> dlQueue;
        private double completionPercentage = 0.0;
        private int totalDownloads = 0;

        public event ProgressUpdatedEventHandler ProgressUpdated;

        public DownloadManager()
        {
            dlQueue = new Stack<IDownload>();
        }

        /// <summary>
        /// Add a Download to the DownloadQueue
        /// </summary>
        /// <param name="download">Download to add to the queue</param>
        public void AddToQueue(IDownload download)
        {
            dlQueue.Push(download);
            totalDownloads++;
        }

        /// <summary>
        /// Start the download process of the next item in the queue
        /// </summary>
        public void DownloadNext()
        {
            if (dlQueue.Count <= 0)
            {
                // reset
                totalDownloads = 0;
                completionPercentage = 0.0;
                return;
            }

            IDownload d = dlQueue.Pop();
            d.DownloadCompleted += new DownloadCompletedEventHandler(DLCompletedCallback);
            d.StartDownloading();
        }

        private void DLCompletedCallback(object sender)
        {
            // Try to download the next item in the queue, if applicable
            if (totalDownloads != 0)
            {
                completionPercentage = ((totalDownloads - dlQueue.Count) / (double)totalDownloads) * 100;
                ProgressUpdated.Invoke(this);
            }
            DownloadNext();
        }

        /// <summary>
        /// Get the percentage of the download that is complete
        /// </summary>
        /// <returns></returns>
        public double GetCompletionPercentage()
        {
            return completionPercentage;
        }
    }
}
