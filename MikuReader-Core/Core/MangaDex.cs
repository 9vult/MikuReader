﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    /// <summary>
    /// Helper class for working with MangaDex
    /// </summary>
    public class MangaDex
    {
        /// <summary>
        /// Get the JSON string for a Manga
        /// </summary>
        /// <param name="mangaUrl">Manga URL</param>
        /// <returns>JSON string</returns>
        public static string GetMangaJSON(string mangaUrl)
        {
            string apiUrl = "https://mangadex.cc/api/manga/" + GetMangaID(mangaUrl);
            string json;
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString(apiUrl);
            }
            return json;
        }

        /// <summary>
        /// Get the JSON string for a Chapter
        /// </summary>
        /// <param name="chapterID">Chapter URL</param>
        /// <returns>JSON string</returns>
        public static string GetChapterJSON(string chapterID)
        {
            string apiUrl = "https://mangadex.cc/api/chapter/" + chapterID;
            string json;
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString(apiUrl);
            }
            return json;
        }

        /// <summary>
        /// Get the ID of a Manga from its URL
        /// </summary>
        /// <param name="mangaUrl">Manga URL</param>
        /// <returns>Manga ID</returns>
        public static string GetMangaID(string mangaUrl)
        {
            try
            {
                return mangaUrl.Split('/')[5];
            } catch (Exception)
            {
                return mangaUrl.Split('/')[4];
            }
        }

        public static string GetMangaUrl(string mangaID)
        {
            return "https://mangadex.cc/title/" + mangaID;
        }


    }
}
