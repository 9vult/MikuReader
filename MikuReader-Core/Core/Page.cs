using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    public class Page
    {
        private readonly string filepath;

        public Page(string filepath)
        {
            this.filepath = filepath;
        }

        public string GetPath()
        {
            return filepath;
        }
    }
}
