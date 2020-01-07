using System;
using System.Collections.Generic;
using System.Text;

namespace MikuReader.Core.Etc
{
    /// <summary>
    /// Logging class for MikuReader
    /// </summary>
    public class Logger
    {
        private List<string> backlog;

        /// <summary>
        /// Create a new instance of the Logger
        /// </summary>
        public Logger()
        {
            backlog = new List<string>();
            Log("Logger Initialized");
        }

        /// <summary>
        /// Add a message to the log
        /// </summary>
        /// <param name="message">Message to add</param>
        public void Log(string message)
        {
            backlog.Add(Time + " [LOG]  " + message);
        }

        /// <summary>
        /// Add a warning to the log
        /// </summary>
        /// <param name="message">Warning to add</param>
        public void Warn(string message)
        {
            backlog.Add(Time + " [WARN]  " + message);
        }

        /// <summary>
        /// Add an error to the log
        /// </summary>
        /// <param name="message">Error to add</param>
        public void Error(string message)
        {
            backlog.Add(Time + " [ERROR]  " + message);
        }

        /// <summary>
        /// Get the backlog
        /// </summary>
        public List<string> GetLog => backlog;

        /// <summary>
        /// Get the backlog as a string seperated by newlines
        /// </summary>
        public string GetLogAsString => string.Join("\n", backlog);

        /// <summary>
        /// Get the instance of the logger
        /// </summary>
        public Logger Get => this;

        /// <summary>
        /// Get the current time
        /// </summary>
        private string Time => DateTime.Now.ToString("HH:mm:ss tt");

    }
}
