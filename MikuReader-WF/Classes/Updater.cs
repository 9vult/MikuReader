using AutoUpdaterDotNET;
using MikuReader.Core;
using MikuReader.wf.Forms;
using Newtonsoft.Json;
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

            string json;
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString("https://raw.githubusercontent.com/ninevult/MikuReader/master/MikuReader-WF/updater/wfupdate.json");
            }
            UpdateInfo info = JsonConvert.DeserializeObject<UpdateInfo>(json);

            if (float.Parse(info.Update_major) > float.Parse(WFClient.CLIENT_MAJOR)
                || float.Parse(info.Update_minor) > float.Parse(WFClient.CLIENT_MINOR))
            {
                new FrmUpdate(info).Show();
            }

            // AutoUpdater.Start("https://raw.githubusercontent.com/ninevult/MikuReader/master/MikuReader-WF/updater/wfupdate.xml");
            // AutoUpdater.RunUpdateAsAdmin = true;
            // AutoUpdater.OpenDownloadPage = true;
        }
    }
}
