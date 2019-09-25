using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    public class FileHelper
    {
        /// <summary>
        /// Application root directory.  Defaults to AppData/Roaming/MikuReader2
        /// </summary>
        public static DirectoryInfo APP_ROOT;

        /// <summary>
        /// Retrieves a file and returns the path
        /// </summary>
        /// <param name="parent">Parent folder to look in</param>
        /// <param name="filename">Name of the file</param>
        /// <returns>The filepath</returns>
        public static string GetFilePath(DirectoryInfo parent, string filename)
        {
            FileInfo[] files = parent.GetFiles(filename);
            return (files.Length > 0) ? files[0].FullName : null;
        }

        /// <summary>
        /// Retrieves a file
        /// </summary>
        /// <param name="parent">Parent folder to look in</param>
        /// <param name="filename">Name of the file</param>
        /// <returns>FileInfo object for the file</returns>
        public static FileInfo GetFile(DirectoryInfo parent, string filename)
        {
            FileInfo[] files = parent.GetFiles(filename);
            return (files.Length > 0) ? files[0] : null;
        }

        /// <summary>
        /// Gets all the files in the parent folder
        /// </summary>
        /// <param name="parent">Parent folder</param>
        /// <returns>FileInfo array with all the files</returns>
        public static FileInfo[] GetFiles(DirectoryInfo parent)
        {
            return parent.GetFiles("*");
        }

        /// <summary>
        /// Gets all directories in the parent folder
        /// </summary>
        /// <param name="parent">Parent folder</param>
        /// <returns>DirectoryInfo array with all the directories</returns>
        public static DirectoryInfo[] GetDirs(DirectoryInfo parent)
        {
            return parent.GetDirectories();
        }

        /// <summary>
        /// Creates a folder
        /// </summary>
        /// <param name="parent">Parent folder to create the new folder in</param>
        /// <param name="name">Name of the folder</param>
        /// <returns>DirectoryInfo object for the created folder</returns>
        public static DirectoryInfo CreateFolder(DirectoryInfo parent, string name)
        {
            return parent.CreateSubdirectory(name);
        }

        /// <summary>
        /// Creates a DirectoryInfo object given a path
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <returns>DirectoryInfo object for the given path</returns>
        public static DirectoryInfo CreateDI(string path)
        {
            return new DirectoryInfo(path);
        }
    }
}
