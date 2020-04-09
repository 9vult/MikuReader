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
    public class Nhentai : Hentai
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
        public Nhentai(DirectoryInfo location)
        {
            this.hentaiRoot = location;
            chapters = new List<Chapter>();
            _Load();
        }

        /// <summary>
        /// Create a new manga and its files
        /// </summary>
        /// <param name="jsonText">JSON Text for this Hentai</param>
        public Nhentai(DirectoryInfo location, string jsonText)
        {
            JObject jobj = JObject.Parse(jsonText);

            this.hentaiRoot = location;
            this.name = (string)jobj["hentai"]["title"];
            this.id = (string)jobj["hentai"]["num"];
            string url = (string)jobj["hentai"]["url"];

            chapters = new List<Chapter>();
            _Create(url);
        }

        /// <summary>
        /// Loads Manga info from manga.txt
        /// </summary>
        public override void _Load()
        {
            string input = File.ReadAllText(FileHelper.GetFilePath(hentaiRoot, "manga.json"));
            MangaInfo info = JsonConvert.DeserializeObject<MangaInfo>(input);

            id = info.Id;
            name = info.Name;
            usertitle = info.UserName;
            currentpage = info.Page;

            _PopulateChapters();
        }

        /// <summary>
        /// Creates manga.txt, then calls Load()
        /// </summary>
        /// <param name="mangaUrl"></param>
        public override void _Create(string mangaUrl)
        {
            FileHelper.CreateFolder(FileHelper.APP_ROOT, "h" + id);

            MangaInfo info = new MangaInfo()
            {
                Type = "hentai",
                Source = "nhentai",
                Id = id,
                Name = name,
                UserName = usertitle,
                Chapter = "1",
                Page = "1"
            };

            string output = JsonConvert.SerializeObject(info);            
            File.WriteAllText(Path.Combine(hentaiRoot.FullName, "manga.json"), output);

            DirectoryInfo chapDir = FileHelper.CreateFolder(hentaiRoot, "1");
            chapters.Add(new Chapter(chapDir, id, "1", true));

            _Load();
        }

        /// <summary>
        /// Create a Chapter for each chapter and add it to the chapter list
        /// </summary>
        public override void _PopulateChapters()
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

            MangaInfo info = new MangaInfo()
            {
                Type = "hentai",
                Source = "nhentai",
                Id = hentaiRoot.Name,
                Name = name,
                UserName = usertitle,
                Chapter = chapter,
                Page = page
            };

            string output = JsonConvert.SerializeObject(info);
            File.WriteAllText(Path.Combine(hentaiRoot.FullName, "manga.json"), output);
        }

        public override void UpdateProperties(string title, string lang, string group)
        {
            this.usertitle = title;
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
            FileInfo[] files = FileHelper.GetFiles(hentaiRoot);
            foreach (FileInfo f in files)
                if (f.Name.StartsWith("dl"))
                    return true;
            return false;
        }
    }
}
