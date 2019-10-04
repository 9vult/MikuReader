using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.wf
{
    public class WFClient
    {
        public static readonly string CLIENT_VERSION = "2.0";

        public static DatabaseManager dbm;
        public static DownloadManager dlm;

        public static void Initialize()
        {
            dbm = new DatabaseManager();
            dlm = new DownloadManager();
        }
    }
}
