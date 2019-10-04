using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Provides methods for managing Databases
    /// </summary>
    public class DatabaseManager
    {
        private MangaDB mangaDB;

        public DatabaseManager()
        {
            this.mangaDB = new MangaDB();
        }

        /// <summary>
        /// Populate the databases
        /// </summary>
        private void Populate()
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

        /// <summary>
        /// Refresh the databases
        /// </summary>
        public void Refresh()
        {
            mangaDB.Clear();

            Populate();
        }

        /// <summary>
        /// Gets all Manga from the Manga database
        /// </summary>
        /// <returns></returns>
        public List<Title> GetMangaPopulation()
        {
            return mangaDB.Get();
        }

        /// <summary>
        /// Get the current Manga Database
        /// </summary>
        /// <returns>The current Manga Database</returns>
        public MangaDB GetMangaDB()
        {
            return this.mangaDB;
        }
    }
}
