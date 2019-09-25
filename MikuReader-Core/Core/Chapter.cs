using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    public class Chapter
    {
        private readonly DirectoryInfo chapterRoot;
        private readonly ArrayList pages;
        private readonly string chapterNumber;

        public Chapter(DirectoryInfo chapterRoot)
        {
            this.pages = new ArrayList();
            this.chapterRoot = chapterRoot;
            this.chapterNumber = chapterRoot.Name;
            foreach (FileInfo fi in FileHelper.GetFiles(chapterRoot))
            {
                pages.Add(new Page(fi.FullName));
            }
        }

        public string GetNumber()
        {
            return chapterNumber;
        }

        public DirectoryInfo GetChapterRoot()
        {
            return chapterRoot;
        }
    }
}
