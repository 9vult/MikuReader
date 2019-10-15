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
    public class Hentai : Title
    {
        private string id;
        private string name;
        private string usertitle;
        private string currentchapter;
        private string currentpage;

        private List<Chapter> chapters;
        private DirectoryInfo hentaiRoot;

        /// <summary>
        /// Create a new Hentai when the files exist
        /// </summary>
        /// <param name="location">Root directory for this Hentai</param>
        public Hentai(DirectoryInfo location)
        {
            this.hentaiRoot = location;
            chapters = new List<Chapter>();
            Load();
        }

        /// <summary>
        /// Create a new manga and its files
        /// </summary>
        /// <param name="jsonText">JSON Text for this Hentai</param>
        public Hentai(DirectoryInfo location, string jsonText)
        {
            JObject jobj = JObject.Parse(jsonText);

            this.hentaiRoot = location;
            this.name = (string)jobj["hentai"]["title"];
            this.id = (string)jobj["hentai"]["num"];
            string url = (string)jobj["hentai"]["url"];

            chapters = new List<Chapter>();
            Create(url);
        }

        /// <summary>
        /// Loads Manga info from manga.txt
        /// </summary>
        private void Load()
        {
            string[] info = File.ReadAllLines(FileHelper.GetFilePath(hentaiRoot, "manga.txt"));
            if (info.Length < 5) { throw new FileLoadException("'manga.txt' did not contain all required fields!"); }
            // info[0] is the type identifier
            id = info[1];
            name = info[2];
            usertitle = info[3];
            // info[4] is the chapter (always 1)
            currentpage = info[5];

            PopulateChapters();
        }

        /// <summary>
        /// Creates manga.txt, then calls Load()
        /// </summary>
        /// <param name="mangaUrl"></param>
        private void Create(string mangaUrl)
        {
            FileHelper.CreateFolder(FileHelper.APP_ROOT, "h" + id);
            File.WriteAllLines(Path.Combine(hentaiRoot.FullName, "manga.txt"), new string[] {
                "hentai",
                id,
                name,
                name, // TODO: Custom user title
                "1", "1" // Chapter 1, page 1
            });
            DirectoryInfo chapDir = FileHelper.CreateFolder(hentaiRoot, "1");
            chapters.Add(new Chapter(chapDir, id, "1"));

            Load();
        }

        /// <summary>
        /// Create a Chapter for each chapter and add it to the chapter list
        /// </summary>
        private void PopulateChapters()
        {
            foreach (DirectoryInfo di in FileHelper.GetDirs(hentaiRoot))
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

            File.WriteAllLines(Path.Combine(hentaiRoot.FullName, "manga.txt"), new string[] {
                "hentai",
                hentaiRoot.Name,
                name,
                usertitle, // TODO: Custom user title
                chapter, page
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

    }
}
