using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// EventHandler for notifying subscribers of the completion of this Download
    /// </summary>
    /// <param name="sender"></param>
    public delegate void DownloadCompletedEventHandler(object sender);

    /// <summary>
    /// Generic Download interface
    /// </summary>
    public interface IDownload
    {
        /// <summary>
        /// Event for the completion of the Download
        /// </summary>
        event DownloadCompletedEventHandler DownloadCompleted;

        /// <summary>
        /// Start the download procedure
        /// </summary>
        void StartDownloading();

        /// <summary>
        /// Begin the async download process
        /// </summary>
        /// <param name="imgUrl">URL to retrieve the image from</param>
        /// <param name="imgFile">Location to save the image to</param>
        void DownloadAsync(Uri imgUrl, string imgFile);

        /// <summary>
        /// Check if the next sub-download should be started
        /// </summary>
        /// <returns>True if the next sub-download should be started</returns>
        bool CheckStartNext();

        /// <summary>
        /// Callback for the async download
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DownloadCompletedCallback(object sender, AsyncCompletedEventArgs e);

        /// <summary>
        /// Get the progress of the current download
        /// </summary>
        /// <returns>The progress of the current download</returns>
        int GetProgress();


    }
}
