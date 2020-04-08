using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikuReader.Core
{
    public class KissMangaHelper
    {
        public static readonly string KISS_URL = "https://kissmanga.org";

        public static string GetName(string mUrl)
        {
            var document = new HtmlWeb().Load(mUrl);
            int startIndex = document.DocumentNode.InnerHtml.IndexOf("\"bigChar\">") + 10;
            int length = 150; // Hope no title is longer than 150 characters
            string crop = document.DocumentNode.InnerHtml.Substring(startIndex, length);

            int end = crop.IndexOf("</strong>");
            return crop.Substring(0, end);
        }

        public static string GetUrlName(string mUrl)
        {
            // https://kissmanga.org/manga/fruits_basket
            return mUrl.Split('/')[4];
        }

        public static string[] GetChapterUrls(string mUrl)
        {
            var document = new HtmlWeb().Load(mUrl);
            string[] urls = document.DocumentNode.Descendants("a")
                            .Select(e => e.GetAttributeValue("href", null))
                            .Where(s => !string.IsNullOrEmpty(s)).ToArray<string>();

            List<string> chapterUrls = new List<string>();

            foreach (string url in urls)
            {
                if (url.StartsWith("/chapter/"))
                {
                    chapterUrls.Add(url);
                }
            }

            chapterUrls.Reverse(); // Switch to increasing (1->2)

            return chapterUrls.ToArray();
        }

        public static string[] GetPageUrls(string cUrl)
        {
            var document = new HtmlWeb().Load(cUrl);
            string[] urls = document.DocumentNode.Descendants("img")
                            .Select(e => e.GetAttributeValue("src", null))
                            .Where(s => !string.IsNullOrEmpty(s)).ToArray<string>();

            List<string> pageUrls = new List<string>();

            foreach (string url in urls)
            {
                if (!url.Contains("/static/"))
                {
                    pageUrls.Add(url);
                }
            }

            return pageUrls.ToArray();
        }

        public static string GetHash(string mUrl)
        {
            return GetUrlName(mUrl); // no hash here
        }

        public static string GetPageFileName(string pUrl)
        {
            string[] splits = pUrl.Split('/');
            return splits[splits.Length - 1];
        }
    }
}
