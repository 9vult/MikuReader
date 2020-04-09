using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MikuReader.Core
{
    /// <summary>
    /// Representation of a Manga
    /// </summary>
    public abstract class Manga : Title
    {

        /// <summary>
        /// Loads Manga info from manga.txt
        /// </summary>
        public abstract void _Load(bool doChapters);

        /// <summary>
        /// Creates manga.txt, then calls Load()
        /// </summary>
        /// <param name="mangaUrl"></param>
        public abstract void _Create(string mangaUrl);

        public abstract Chapter[] GetSetPrunedChapters(bool overrideDlc);

        public abstract Chapter[] GetUpdates();

        /// <summary>
        /// Get the groups associated with this manga
        /// </summary>
        /// <param name="langCode">Language to select group from</param>
        /// <returns>List of groups associated with the language</returns>
        public abstract string[] GetGroups(string langCode);

        public abstract string[] GetLangs();

        /// <summary>
        /// Get the user's specified language
        /// </summary>
        public abstract string GetUserLang();

        /// <summary>
        /// Get the user's specified Group
        /// </summary>
        public abstract string GetUserGroup();

        /// <summary>
        /// Create a Chapter for each chapter and add it to the chapter list
        /// </summary>
        public abstract void _PopulateChapters();

        public abstract override void UpdateLocation(string chapter, string page);

        public abstract override void Save(string chapter, string page);

        public abstract override void UpdateProperties(string title, string lang, string group);

        public abstract override string GetTitle();

        public abstract override string GetUserTitle();

        public abstract override List<Chapter> GetChapters();

        public abstract override string GetCurrentChapter();

        public abstract override string GetCurrentPage();

        public abstract override string GetID();

        public abstract override bool IsDownloading();

        public abstract void UpdateDLChapters(String[] chapterNums);

        public abstract String[] GetDLChapters();

        public static MangaType GetSource(DirectoryInfo dir)
        {
            try
            {
                string input = File.ReadAllText(FileHelper.GetFilePath(dir, "manga.json"));
                MangaInfo info = JsonConvert.DeserializeObject<MangaInfo>(input);
                switch (info.Source)
                {
                    case "mangadex":
                        return MangaType.MANGADEX;
                    case "kissmanga":
                        return MangaType.KISSMANGA;
                    default:
                        return MangaType.NULL;
                }
            }
            catch (Exception)
            {
                return MangaType.NULL;
            }
        }

    }
}
