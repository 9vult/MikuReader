using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
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

        public Manga(DirectoryInfo location)
        {
            this.mangaRoot = location;
            Load();
        }

        public Manga(DirectoryInfo location, string mangaUrl)
        {
            this.mangaRoot = location;
            Create(mangaUrl);
        }

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

        private void Create(string mangaUrl)
        {
            string jsonText = MangaDex.GetMangaJSON(mangaUrl);
            dynamic jsonContents = JsonConvert.DeserializeObject(jsonText);
            FileHelper.CreateFolder(FileHelper.APP_ROOT, MangaDex.GetMangaID(mangaUrl));
            File.WriteAllLines(mangaRoot.FullName + "manga.txt", new string[] {
                "manga",
                jsonContents.manga.title,
                "gb", // TODO: Custom user languages
                "^any-group", // TODO: Custom user groups
                jsonContents.manga.title, // TODO: Custom user title
                "1", "1", // Chapter 1, page 1
                "1" // TODO: Get latest chapter for language and group
            });
            Load();
        }

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

        public override string GetTitle()
        {
            return this.name;
        }

        public override string GetUserTitle()
        {
            return this.usertitle;
        }

    }
}
