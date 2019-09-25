using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    class MangaDex
    {
        public static string GetMangaJSON(string mangaUrl)
        {
            string apiUrl = "https://mangadex.org/api/manga/" + GetMangaID(mangaUrl);
            string json;
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString(apiUrl);
            }
            return json;
        }

        public static string GetChapterJSON(string chapterID)
        {
            string apiUrl = "https://mangadex.org/api/chapter/" + chapterID;
            string json;
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString(apiUrl);
            }
            return json;
        }

        public static string GetMangaID(string mangaUrl)
        {
            return mangaUrl.Split('/')[4];
        }
    }
}
