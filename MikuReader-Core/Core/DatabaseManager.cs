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
        private HentaiDB hentaiDB;

        public DatabaseManager()
        {
            this.mangaDB = new MangaDB();
            this.hentaiDB = new HentaiDB();
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

                    case TitleType.HENTAI:
                        hentaiDB.Add(new Hentai(dir));
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
            hentaiDB.Clear();

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
        /// Gets all Hentai from the Hentai database
        /// </summary>
        /// <returns></returns>
        public List<Title> GetHentaiPopulation()
        {
            return hentaiDB.Get();
        }

        /// <summary>
        /// Get the current Manga Database
        /// </summary>
        /// <returns>The current Manga Database</returns>
        public MangaDB GetMangaDB()
        {
            return this.mangaDB;
        }

        /// <summary>
        /// Get the current Hentai Database
        /// </summary>
        /// <returns>The current Hentai Database</returns>
        public HentaiDB GetHentaiDB()
        {
            return this.hentaiDB;
        }
    }
}
