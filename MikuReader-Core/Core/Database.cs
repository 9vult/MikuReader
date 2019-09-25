using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    public abstract class Database
    {
        public abstract void Add(Title title);
        public abstract List<Title> Get();
        public abstract Title Get(string name);
        public abstract Title Remove(string name);
        public abstract void Clear();
    }
}
