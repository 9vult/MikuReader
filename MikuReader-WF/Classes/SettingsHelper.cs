using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.wf.Classes
{
    public class SettingsHelper
    {
        public static bool UseDoubleReader { get; set; }
        public static bool CheckForUpdates { get; set; }

        /// <summary>
        /// Initialize the SettingsHelper
        /// </summary>
        public static void Initialize()
        {
            string[] info = File.ReadAllLines(FileHelper.GetFile(FileHelper.APP_ROOT, "mikureader.txt").FullName);
            if (info.Length < 2) { throw new FileLoadException("'mikureader.txt' did not contain all required fields!"); }


            UseDoubleReader = bool.Parse(info[0].Split('/')[0].Trim());
            CheckForUpdates = bool.Parse(info[1].Split('/')[0].Trim());
        }

        /// <summary>
        /// Save settings to settings file
        /// </summary>
        public static void Save()
        {
            File.WriteAllLines(FileHelper.GetFile(
                FileHelper.APP_ROOT, "mikureader.txt").FullName,
                new string[] {
                    UseDoubleReader.ToString() + " // Use Double-Page Reader",
                    CheckForUpdates.ToString() + " // Check for updates on startup"
                }
            );
        }

        /// <summary>
        /// Create the settings file
        /// </summary>
        public static void Create()
        {
            File.WriteAllLines(Path.Combine(
                FileHelper.APP_ROOT.FullName, "mikureader.txt"),
                new string[] {
                    "False // Use Double-Page Reader",
                    "True // Check for updates on startup"
                }
            );
        }

    }
}
