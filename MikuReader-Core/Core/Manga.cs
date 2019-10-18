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
    public class Manga : Title
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

        /// <summary>
        /// Create a new Manga when the files exist
        /// </summary>
        /// <param name="location">Root directory for this Manga</param>
        public Manga(DirectoryInfo location)
        {
            this.mangaRoot = location;
            chapters = new List<Chapter>();
            Load();
        }

        /// <summary>
        /// Create a new manga and its files
        /// </summary>
        /// <param name="location">Root directory for this Manga</param>
        /// <param name="mangaUrl">MangaDex URL</param>
        public Manga(DirectoryInfo location, string mangaUrl)
        {
            this.mangaRoot = location;
            chapters = new List<Chapter>();
            Create(mangaUrl);
        }

        /// <summary>
        /// Loads Manga info from manga.txt
        /// </summary>
        private void Load()
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

            PopulateChapters();
        }

        /// <summary>
        /// Creates manga.txt, then calls Load()
        /// </summary>
        /// <param name="mangaUrl"></param>
        private void Create(string mangaUrl)
        {
            string jsonText = MangaDex.GetMangaJSON(mangaUrl);

            JObject jobj = JObject.Parse(jsonText);
            string title = (string)jobj["manga"]["title"];

            string lang_code = "gb";

            FileHelper.CreateFolder(FileHelper.APP_ROOT, MangaDex.GetMangaID(mangaUrl));
            File.WriteAllLines(Path.Combine(mangaRoot.FullName, "manga.txt"), new string[] {
                "manga",
                MangaDex.GetMangaID(mangaUrl),
                title,
                lang_code, // TODO: Custom user languages
                "^any-group", // TODO: Custom user groups
                title, // TODO: Custom user title
                "1", "1", // Chapter 1, page 1
                "1" // TODO: Get latest chapter for language and group
            });

            foreach (JProperty p in jobj["chapter"])
            {
                JToken value = p.Value;
                if (value.Type == JTokenType.Object)
                {
                    JObject o = (JObject)value;
                    string chapNum = (String)o["chapter"];
                    if (((string)o["lang_code"]).Equals(lang_code))
                    {
                        // Console.WriteLine(chapNum);
                        string chapID = ((JProperty)value.Parent).Name;
                        DirectoryInfo chapDir = FileHelper.CreateFolder(mangaRoot, chapID);
                        chapters.Add(new Chapter(chapDir, chapID, chapNum));
                    }
                }

            }

            Load();
        }

        public Chapter[] GetUpdates()
        {
            List<Chapter> result = new List<Chapter>();
            string jsonText = MangaDex.GetMangaJSON(MangaDex.GetMangaUrl(GetID()));
            JObject jobj = JObject.Parse(jsonText);

            foreach (JProperty p in jobj["chapter"])
            {
                JToken value = p.Value;
                if (value.Type == JTokenType.Object)
                {
                    JObject o = (JObject)value;
                    string chapNum = (String)o["chapter"];
                    if (((string)o["lang_code"]).Equals(userlang))
                    {
                        // Console.WriteLine(chapNum);
                        string chapID = ((JProperty)value.Parent).Name;
                        if (!Directory.Exists(Path.Combine(mangaRoot.FullName, chapID)))
                        {
                            DirectoryInfo chapDir = FileHelper.CreateFolder(mangaRoot, chapID);
                            Chapter newchapter = new Chapter(chapDir, chapID, chapNum);
                            chapters.Add(newchapter);
                            result.Add(newchapter);
                        }
                    }
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Create a Chapter for each chapter and add it to the chapter list
        /// </summary>
        private void PopulateChapters()
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
                "gb", // TODO: Custom user languages
                "^any-group", // TODO: Custom user groups
                usertitle, // TODO: Custom user title
                chapter, page,
                lastchapter // TODO: Get latest chapter for language and group
            });
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

    }
}
