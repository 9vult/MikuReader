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
        private string name;
        private string userlang;
        private string usergroup;
        private string usertitle;
        private string currentchapter;
        private string currentpage;
        private string lastchapter;

        private ArrayList chapters;

        private DirectoryInfo mangaRoot;

        /// <summary>
        /// Create a new Manga when the files exist
        /// </summary>
        /// <param name="location">Root directory for this Manga</param>
        public Manga(DirectoryInfo location)
        {
            this.mangaRoot = location;
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
            Create(mangaUrl);
        }

        /// <summary>
        /// Loads Manga info from manga.txt
        /// </summary>
        private void Load()
        {
            chapters = new ArrayList();
            string[] info = File.ReadAllLines(FileHelper.GetFilePath(mangaRoot, "manga.txt"));
            if (info.Length < 8) { throw new FileLoadException("'manga.txt' did not contain all required fields!"); }
            // info[0] is the type identifier
            name = info[1];
            userlang = info[2];
            usergroup = info[3];
            usertitle = info[4];
            currentchapter = info[5];
            currentpage = info[6];
            lastchapter = info[7];

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

            FileHelper.CreateFolder(FileHelper.APP_ROOT, MangaDex.GetMangaID(mangaUrl));
            File.WriteAllLines(mangaRoot.FullName + "manga.txt", new string[] {
                "manga",
                title,
                "gb", // TODO: Custom user languages
                "^any-group", // TODO: Custom user groups
                title, // TODO: Custom user title
                "1", "1", // Chapter 1, page 1
                "1" // TODO: Get latest chapter for language and group
            });
            Load();
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

            File.WriteAllLines(mangaRoot.FullName + "manga.txt", new string[] {
                "manga",
                name,
                "gb", // TODO: Custom user languages
                "^any-group", // TODO: Custom user groups
                usertitle, // TODO: Custom user title
                chapter, page, // Chapter 1, page 1
                "1" // TODO: Get latest chapter for language and group
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

        public override ArrayList GetChapters()
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

    }
}
