using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader
{
    public class Manga
    {
        public string name;
        public DirectoryInfo mangaDirectory;
        public string currentChapter;
        public string currentPage;
        public MangaSettingsContainer settings;

        public Manga(string name, DirectoryInfo mangaDirectory, string currentChapter, string currentPage)
        {
            this.name = name;
            this.mangaDirectory = mangaDirectory;
            this.currentChapter = currentChapter;
            this.currentPage = currentPage;
        }

        private void CreateSettings()
        {
            Logger.Log("Creating settings for manga '" + name + "'");
            File.WriteAllText(mangaDirectory.FullName + "\\settings",
                Properties.Settings.Default["languageCode"].ToString() + "|{Any}");
            LoadSettings();
        }

        public bool LoadSettings()
        {
            Logger.Log("Loading settings for manga '" + name + "'");
            if (!File.Exists(mangaDirectory.FullName + "\\settings"))
            {
                Logger.Warn("Settings file did not exist");
                //File.Create(mangaDirectory.FullName + "\\settings");
                CreateSettings();
                return false;
            }

            string file = File.ReadAllText(mangaDirectory.FullName + "\\settings");

            string[] units = file.Split('|');

            if (settings != null)
                settings = null;

            if (units.Length < 3)
            {
                Logger.Warn("Settings length < 3, creating blank name");
                string[] temp = new string[3];
                temp[0] = units[0];
                temp[1] = units[1];
                temp[2] = "";
                units = temp;
            }

            settings = new MangaSettingsContainer
            {
                lang = units[0],
                group = units[1],
                name = units[2]
            };
            return true;
        }

        public void SaveSettings(string lang, string group, string overrideName)
        {
            Logger.Log("Saving settings for manga '" + name + "'");
            if (File.Exists(mangaDirectory.FullName + "\\settings"))
            {
                File.WriteAllText(mangaDirectory.FullName + "\\settings", String.Empty);
                File.WriteAllText(mangaDirectory.FullName + "\\settings", lang + "|" + group + "|" + overrideName);
            }
        }

        public string MangaDirectory => mangaDirectory.FullName;
    }
}
