using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    public class DatabaseManager
    {
        private MangaDB mangaDB;

        public DatabaseManager()
        {
            this.mangaDB = new MangaDB();
        }

        public void Populate()
        {
            foreach (DirectoryInfo dir in FileHelper.GetDirs(FileHelper.APP_ROOT))
            {
                switch (Title.GetType(dir))
                {
                    case TitleType.MANGA:
                        mangaDB.Add(new Manga(dir));
                        break;

                    case TitleType.NULL:
                    default:
                        break;

                }
            }
        }

        public void Refresh()
        {
            mangaDB.Clear();

            Populate();
        }

        public List<Title> GetMangaPopulation()
        {
            return mangaDB.Get();
        }
    }
}
