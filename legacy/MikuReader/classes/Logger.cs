using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader
{
    public class Logger
    {
        public static string logFile;

        /// <summary>
        /// Adds a Log entry to the log
        /// </summary>
        /// <param name="message">Log entry</param>
        public static void Log(string message)
        {
            File.AppendAllText(logFile, "[Log] " + GetTime() + " " + message + "\n");
        }

        /// <summary>
        /// Adds a Warn entry to the log
        /// </summary>
        /// <param name="message">Log entry</param>
        public static void Warn(string message)
        {
            File.AppendAllText(logFile, "[Warn] " + GetTime() + " " + message + "\n");
        }

        /// <summary>
        /// Adds an Error entry to the log
        /// </summary>
        /// <param name="message">Log entry</param>
        public static void Error(string message)
        {
            File.AppendAllText(logFile, "[Error] " + GetTime() + " " + message + "\n");
        }

        /// <summary>
        /// Gets the current time in Date + Time format
        /// </summary>
        /// <returns>Date and Time</returns>
        private static string GetTime() => "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "]";

    }
}
