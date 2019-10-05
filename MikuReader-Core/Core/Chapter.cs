using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Representation of a Chapter (of a Title)
    /// </summary>
    public class Chapter
    {
        private readonly DirectoryInfo chapterRoot;
        private readonly List<Page> pages;
        private string chapterID;
        private string chapterNum;

        /// <summary>
        /// Create a new Chapter
        /// </summary>
        /// <param name="chapterRoot">Root directory for this chapter</param>
        public Chapter(DirectoryInfo chapterRoot)
        {
            this.pages = new List<Page>();
            this.chapterRoot = chapterRoot;

            Load();
        }

        /// <summary>
        /// Create a new Chapter
        /// </summary>
        /// <param name="chapterRoot">Root directory for this chapter</param>
        /// <param name="id">Chapter ID</param>
        /// <param name="num">Chapter number</param>
        public Chapter(DirectoryInfo chapterRoot, string id, string num)
        {
            this.pages = new List<Page>();
            this.chapterRoot = chapterRoot;

            Create(id, num);
        }


        private void Load()
        {
            string[] info = File.ReadAllLines(FileHelper.GetFilePath(chapterRoot, "chapter.txt"));
            if (info.Length < 3) { throw new FileLoadException("'chapter.txt' did not contain all required fields!"); }
            // info[0] is the type identifier
            chapterID = info[1];
            chapterNum = info[2];

            PopulatePages();
        }

        private void Create(string id, string num)
        {
            File.WriteAllLines(Path.Combine(chapterRoot.FullName, "chapter.txt"), new string[] {
                "chapter",
                id,
                num,
            });

            Load();
        }

        private void PopulatePages()
        {
            foreach (FileInfo fi in FileHelper.GetFiles(chapterRoot))
            {
                if (fi.Extension != ".txt")
                    pages.Add(new Page(fi.FullName));
            }
        }

        /// <summary>
        /// Get the ID of the chapter
        /// </summary>
        /// <returns>The chapter ID</returns>
        public string GetID()
        {
            return chapterID;
        }

        /// <summary>
        /// Get the number of the chapter
        /// </summary>
        /// <returns>The chapter number</returns>
        public string GetNum()
        {
            return chapterNum;
        }

        /// <summary>
        /// Get the root directory for this chapter
        /// </summary>
        /// <returns>DirectoryInfo object for the chapter root</returns>
        public DirectoryInfo GetChapterRoot()
        {
            return chapterRoot;
        }

        /// <summary>
        /// Get the list of pages in this chapter
        /// </summary>
        /// <returns>ArrayList of Page objects for this chapter</returns>
        public List<Page> GetPages()
        {
            return pages;
        }

        public Page GetPage(int index)
        {
            return pages[index];
        }
    }
}
