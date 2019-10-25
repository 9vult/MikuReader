using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MikuReader.Mobile
{
    class MClient
    {
        public static readonly string ANDROID_CLIENT_VERSION = "1.0";
        public static readonly string IOS_CLIENT_VERSION = "1.0";

        public static DatabaseManager dbm;
        public static DownloadManager dlm;

        public static void Initialize()
        {
            dbm = new DatabaseManager();
            dlm = new DownloadManager();
        }
    }
}
