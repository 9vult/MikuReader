using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Represents a Title
    /// </summary>
    public abstract class Title
    {
        /// <summary>
        /// Update the user's location in this Title
        /// </summary>
        /// <param name="chapter">User's current Chapter</param>
        /// <param name="page">User's current Page</param>
        public abstract void UpdateLocation(string chapter, string page);

        /// <summary>
        /// Update the user's location in the Title and save the properties file
        /// </summary>
        /// <param name="chapter">User's current Chapter</param>
        /// <param name="page">User's current Page</param>
        public abstract void Save(string chapter, string page);

        /// <summary>
        /// Get the name of the title
        /// </summary>
        /// <returns>The name of this Title</returns>
        public abstract string GetTitle();

        /// <summary>
        /// Get the user-specified nickname for the manga
        /// </summary>
        /// <returns>The User-specified override title</returns>
        public abstract string GetUserTitle();

        /// <summary>
        /// Return the list of chapters
        /// </summary>
        /// <returns>The ArrayList of Chapter objects</returns>
        public abstract List<Chapter> GetChapters();

        /// <summary>
        /// Get the user's current chapter
        /// </summary>
        /// <returns>The user's current chapter</returns>
        public abstract string GetCurrentChapter();

        /// <summary>
        /// Get the user's current page
        /// </summary>
        /// <returns>The user's current page</returns>
        public abstract string GetCurrentPage();

        /// <summary>
        /// Get this title's ID
        /// </summary>
        /// <returns>The ID of the title</returns>
        public abstract string GetID();

        /// <summary>
        /// Get the Type of a Title
        /// </summary>
        /// <param name="dir">Title's directory</param>
        /// <returns></returns>
        public static TitleType GetType(DirectoryInfo dir)
        {
            switch (File.ReadAllLines(FileHelper.GetFilePath(dir, "manga.txt"))[0])
            {
                case "manga":
                    return TitleType.MANGA;
                case "hentai":
                    return TitleType.HENTAI;
                default:
                    return TitleType.NULL;
            }
        }
    }
}
