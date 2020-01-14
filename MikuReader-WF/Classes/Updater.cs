using AutoUpdaterDotNET;
using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.wf.Classes
{
    public class Updater
    {
        public static void Start()
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/ninevult/MikuReader/master/MikuReader-WF/updater/wfupdate.xml");
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.DownloadPath = Path.Combine(FileHelper.APP_ROOT.ToString(), "update");
        }
    }
}
