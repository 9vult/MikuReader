using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Representation of a Page (in a Chapter)
    /// </summary>
    public class Page
    {
        private readonly string filepath;

        /// <summary>
        /// Create a Page
        /// </summary>
        /// <param name="filepath">Page file</param>
        public Page(string filepath)
        {
            this.filepath = filepath;
        }

        /// <summary>
        /// Get the path to the page
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return filepath;
        }

        /// <summary>
        /// Get the ID (page number) of this Page
        /// </summary>
        /// <returns>The ID (page number)</returns>
        public string GetID()
        {
            return Path.GetFileNameWithoutExtension(filepath);
        }
    }
}
