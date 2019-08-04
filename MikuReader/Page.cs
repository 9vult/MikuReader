using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader
{
    class Page
    {

        public string num;
        public FileInfo file;

        public Page(string num, FileInfo file)
        {
            this.num = num;
            this.file = file;
        }

    }
}
