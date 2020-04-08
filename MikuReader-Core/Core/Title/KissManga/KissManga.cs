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
    public class KissManga : Manga
    {
        private string id;
        private string name;
        private string userlang;
        private string usergroup;
        private string usertitle;
        private string currentchapter;
        private string currentpage;
        private string lastchapter;

        private List<Chapter> chapters;
        private DirectoryInfo mangaRoot;

        private readonly MangaType type;

        /// <summary>
        /// Create a new Manga when the files exist
        /// </summary>
        /// <param name="location">Root directory for this Manga</param>
        public KissManga(DirectoryInfo location, MangaType type)
        {
            this.mangaRoot = location;
            this.type = type;
            chapters = new List<Chapter>();
            _Load(true);
        }

        /// <summary>
        /// Create a new manga and its files
        /// </summary>
        /// <param name="location">Root directory for this Manga</param>
        /// <param name="mangaUrl">MangaDex URL</param>
        public KissManga(DirectoryInfo location, string mangaUrl, MangaType type)
        {
            this.mangaRoot = location;
            chapters = new List<Chapter>();
            this.type = type;
            _Create(mangaUrl);
            _Load(true);
        }

        /// <summary>
        /// Loads Manga info from manga.txt
        /// </summary>
        public override void _Load(bool doChapters)
        {
            string[] info = File.ReadAllLines(FileHelper.GetFilePath(mangaRoot, "manga.txt"));
            if (info.Length < 9) { throw new FileLoadException("'manga.txt' did not contain all required fields!"); }
            // info[0] is the type identifier
            id = info[1];
            name = info[2];
            userlang = info[3];
            usergroup = info[4];
            usertitle = info[5];
            currentchapter = info[6];
            currentpage = info[7];
            lastchapter = info[8];

            if (doChapters)
                _PopulateChapters();
        }

        /// <summary>
        /// Creates manga.txt, then calls Load()
        /// </summary>
        /// <param name="mangaUrl"></param>
        public override void _Create(string mangaUrl)
        {
            id = mangaUrl;
            string title = KissMangaHelper.GetName(mangaUrl);
            string lang_code = "gb"; // Not that it matters for KissManga

            FileHelper.CreateFolder(FileHelper.APP_ROOT, KissMangaHelper.GetUrlName(mangaUrl));
            File.WriteAllLines(Path.Combine(mangaRoot.FullName, "manga.txt"), new string[] {
                "manga",
                KissMangaHelper.GetUrlName(mangaUrl),
                title,
                lang_code, // TODO: Custom user languages
                "^any-group", // TODO: Custom user groups
                title, // TODO: Custom user title
                "1", "1", // Chapter 1, page 1
                "1" // TODO: Get latest chapter for language and group
            });

            _Load(false);
            GetSetPrunedChapters(true);
        }

        public override Chapter[] GetSetPrunedChapters(bool overrideDlc)
        {
            chapters.Clear();
            
            List<Chapter> result = new List<Chapter>();



            chapters = result;
            return result.ToArray();
        }

        public override Chapter[] GetUpdates()
        {
            List<Chapter> result = new List<Chapter>();
            string jsonText = MangaDexHelper.GetMangaJSON(MangaDexHelper.GetMangaUrl(GetID()));
            JObject jobj = JObject.Parse(jsonText);

            String[] dlc = GetDLChapters();

            return result.ToArray();
        }

        /// <summary>
        /// Get the groups associated with this manga
        /// </summary>
        /// <param name="langCode">Language to select group from</param>
        /// <returns>List of groups associated with the language</returns>
        public override string[] GetGroups(string langCode)
        {
            return null;
        }

        public override string[] GetLangs()
        {
            return null;
        }

        /// <summary>
        /// Get the user's specified language
        /// </summary>
        public override string GetUserLang() => userlang;

        /// <summary>
        /// Get the user's specified Group
        /// </summary>
        public override string GetUserGroup() => usergroup;

        /// <summary>
        /// Create a Chapter for each chapter and add it to the chapter list
        /// </summary>
        public override void _PopulateChapters()
        {
            foreach (DirectoryInfo di in FileHelper.GetDirs(mangaRoot))
            {
                chapters.Add(new Chapter(di));
            }
        }

        public override void UpdateLocation(string chapter, string page)
        {
            this.currentchapter = chapter;
            this.currentpage = page;
        }

        public override void Save(string chapter, string page)
        {
            this.currentchapter = chapter;
            this.currentpage = page;

            File.WriteAllLines(Path.Combine(mangaRoot.FullName, "manga.txt"), new string[] {
                "manga",
                mangaRoot.Name,
                name,
                userlang,
                usergroup,
                usertitle,
                chapter, page,
                lastchapter // TODO: Get latest chapter for language and group
            });
        }

        public override void UpdateProperties(string title, string lang, string group)
        {
            this.usertitle = title;
            this.userlang = lang;
            if (group == "{Any}") this.usergroup = "^any-group"; else this.usergroup = group;
            Save(currentchapter, currentpage);
        }

        public override string GetTitle()
        {
            return this.name;
        }

        public override string GetUserTitle()
        {
            return this.usertitle;
        }

        public override List<Chapter> GetChapters()
        {
            return chapters;
        }

        public override string GetCurrentChapter()
        {
            return currentchapter;
        }

        public override string GetCurrentPage()
        {
            return currentpage;
        }

        public override string GetID()
        {
            return id;
        }

        public override bool IsDownloading()
        {
            FileInfo[] files = FileHelper.GetFiles(mangaRoot);
            foreach (FileInfo f in files)
                if (f.Name.StartsWith("dl"))
                    return true;
            return false;
        }

        public override void UpdateDLChapters(String[] chapterNums)
        {
            using (StreamWriter file = new StreamWriter(Path.Combine(mangaRoot.FullName, "cdl.txt")))
            {
                foreach (String num in chapterNums)
                {
                    file.WriteLine(num);
                }
            }
        }

        public override String[] GetDLChapters()
        {
            if (File.Exists(Path.Combine(mangaRoot.FullName, "cdl.txt")))
                return File.ReadAllLines(Path.Combine(mangaRoot.FullName, "cdl.txt"));
            else
                return null;
        }

    }
}
