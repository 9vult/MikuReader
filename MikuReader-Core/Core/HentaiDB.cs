using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Implementation of a Database for Manga
    /// </summary>
    public class HentaiDB : Database
    {
        private ArrayList hentais;
        
        public HentaiDB()
        {
            hentais = new ArrayList();
        }

        public override void Add(Title title)
        {
            hentais.Add(title);
        }

        public override void Clear()
        {
            hentais.Clear();
        }

        public override List<Title> Get()
        {
            return hentais.Cast<Title>().ToList();
        }

        public override Title Get(string name)
        {
            foreach (Manga t in hentais)
            {
                if (t.GetTitle().Equals(name))
                {
                    return t;
                }
            }
            return null;
        }

        public override Title Remove(string name)
        {
            foreach (Manga t in hentais)
            {
                if (t.GetTitle().Equals(name))
                {
                    hentais.Remove(t);
                    return t;
                }
            }
            return null;
        }
    }
}
