using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    interface IDownload
    {
        void StartDownloading();
        void DownloadAsync(Uri imgUrl, string imgFile);
        bool CheckStartNext();
        void DownloadCompletedCallback(object sender, AsyncCompletedEventArgs e);
        int GetProgress();
    }
}
