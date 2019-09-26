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
    public class MangaDB : Database
    {
        private ArrayList mangas;
        
        public MangaDB()
        {
            mangas = new ArrayList();
        }

        public override void Add(Title title)
        {
            mangas.Add(title);
        }

        public override void Clear()
        {
            mangas.Clear();
        }

        public override List<Title> Get()
        {
            return mangas.Cast<Title>().ToList();
        }

        public override Title Get(string name)
        {
            foreach (Manga t in mangas)
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
            foreach (Manga t in mangas)
            {
                if (t.GetTitle().Equals(name))
                {
                    mangas.Remove(t);
                    return t;
                }
            }
            return null;
        }
    }
}
