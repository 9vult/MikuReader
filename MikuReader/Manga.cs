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
            File.WriteAllText(mangaDirectory.FullName + "\\settings", 
                Properties.Settings.Default["languageCode"].ToString() + "|{Any}");
            LoadSettings();
        }
        
        public bool LoadSettings()
        {
            if (!File.Exists(mangaDirectory.FullName + "\\settings"))
            {
                //File.Create(mangaDirectory.FullName + "\\settings");
                CreateSettings();
                return false;
            }

            string file = File.ReadAllText(mangaDirectory.FullName + "\\settings");            

            string[] units = file.Split('|');

            if (settings != null)
                settings = null;

            settings = new MangaSettingsContainer
            {
                lang = units[0],
                group = units[1]
            };
            return true;
        }

        public void SaveSettings(string lang, string group)
        {
            if (File.Exists(mangaDirectory.FullName + "\\settings"))
            {
                File.WriteAllText(mangaDirectory.FullName + "\\settings", String.Empty);
                File.WriteAllText(mangaDirectory.FullName + "\\settings", lang + "|" + group);
            }
        }
    }
}
