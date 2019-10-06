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

        /// <summary>
        /// Initialize the SettingsHelper
        /// </summary>
        public static void Initialize()
        {
            string[] info = File.ReadAllLines(FileHelper.GetFile(FileHelper.APP_ROOT, "mikureader.txt").FullName);
            if (info.Length < 1) { throw new FileLoadException("'mikureader.txt' did not contain all required fields!"); }


            UseDoubleReader = bool.Parse(info[0].Split('/')[0].Trim());
        }

        /// <summary>
        /// Save settings to settings file
        /// </summary>
        public static void Save()
        {
            File.WriteAllLines(FileHelper.GetFile(
                FileHelper.APP_ROOT, "mikureader.txt").FullName,
                new string[] {
                    UseDoubleReader.ToString() + " // Use Double-Page Reader"
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
                    "false // Use Double-Page Reader"
                }
            );
        }

    }
}
