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
            WFClient.logger.Log("Checking for updates");
            AutoUpdater.Start("https://raw.githubusercontent.com/ninevult/MikuReader/master/MikuReader-WF/updater/wfupdate.xml");
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.OpenDownloadPage = true;
        }
    }
}
