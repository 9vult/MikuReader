using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader
{
    class Chapter
    {
        public string num;
        public DirectoryInfo dir;
        public Page[] pages;

        public Chapter(string num, DirectoryInfo dir, Page[] pages)
        {
            this.num = num;
            this.dir = dir;
            this.pages = pages;
        }
    }
}
