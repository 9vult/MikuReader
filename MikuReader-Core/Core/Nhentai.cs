using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MikuReader.Core
{
    public class Nhentai
    {
        /// <summary>
        /// Get the image URL
        /// </summary>
        /// <param name="hUrl">NHentai URL</param>
        /// <returns>NHentai Image URL</returns>
        public static string GetImageUrl(string hUrl)
        {
            var document = new HtmlWeb().Load(hUrl);
            string[] urls = document.DocumentNode.Descendants("img")
                                            .Select(e => e.GetAttributeValue("src", null))
                                            .Where(s => !string.IsNullOrEmpty(s)).ToArray<string>();

            foreach (string url in urls)
            {
                Console.WriteLine(url);
                if (url.StartsWith("https://i.nhentai.net/galleries/"))
                    return url;
            }
            // ok then
            return null;
        }

        public static int GetPageCount(string hUrl)
        {
            HtmlDocument document;
            if (hUrl.EndsWith("/"))
                document = new HtmlWeb().Load(hUrl.Substring(0, hUrl.Length - 2));
            else
                document = new HtmlWeb().Load(hUrl.Substring(0, hUrl.Length - 1));

            int startIndex = document.DocumentNode.InnerHtml.IndexOf("\"num_pages\":") + 12;
            int length = 7;

            string crop = document.DocumentNode.InnerHtml.Substring(startIndex, length);

            string pages = Regex.Replace(crop, "[^0-9.]", "");
            return int.Parse(pages);
        }

        public static string GetExt(string imgUrl)
        {
            string[] splits = imgUrl.Split('.');
            return splits[splits.Count() - 1];
        }

        public static string GetHash(string imgUrl)
        {
            // https://i.nhentai.net/galleries/1271133/1.jpg

            string[] splits = imgUrl.Split('/');
            return splits[4];
        }

        public static string ImgBase()
        {
            return "https://i.nhentai.net/galleries/";
        }
    }
}
