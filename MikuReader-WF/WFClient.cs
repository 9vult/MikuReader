using MikuReader.Core;
using MikuReader.Core.Etc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.wf
{
    public class WFClient
    {
        public static readonly string CLIENT_VERSION = "2.0 Beta 3";
        public static readonly string CLIENT_MAJOR = "2.0";
        public static readonly string CLIENT_MINOR = "4.0";

        public static DatabaseManager dbm;
        public static DownloadManager dlm;

        public static Logger logger;

        public static void Initialize()
        {
            dbm = new DatabaseManager();
            dlm = new DownloadManager();

            logger = new Logger();
        }
    }
}
