using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Represents a Database
    /// </summary>
    public abstract class Database
    {
        /// <summary>
        /// Add a title to the database
        /// </summary>
        /// <param name="title">Title to add</param>
        public abstract void Add(Title title);
        
        /// <summary>
        /// Get the entire contents of the database
        /// </summary>
        /// <returns>All titles in the database</returns>
        public abstract List<Title> Get();
        
        /// <summary>
        /// Get a specific title from the database
        /// </summary>
        /// <param name="name">Name of the title to get</param>
        /// <returns>The Title with a matching name</returns>
        public abstract Title Get(string name);
        
        /// <summary>
        /// Remove a specific title from the database
        /// </summary>
        /// <param name="name">Name of the title to remove</param>
        /// <returns>The Title that was removed</returns>
        public abstract Title Remove(string name);
        
        /// <summary>
        /// Empty the database
        /// </summary>
        public abstract void Clear();
    }
}
