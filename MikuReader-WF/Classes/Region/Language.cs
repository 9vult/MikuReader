using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.wf.Classes.Region
{
    public class Language
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public Launcher Launcher { get; set; }
    }
}
