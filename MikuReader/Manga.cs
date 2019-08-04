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

        public Manga(string name, DirectoryInfo mangaDirectory, string currentChapter, string currentPage)
        {
            this.name = name;
            this.mangaDirectory = mangaDirectory;
            this.currentChapter = currentChapter;
            this.currentPage = currentPage;
        }
    }
}
