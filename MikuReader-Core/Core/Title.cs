using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.Core
{
    public abstract class Title
    {
        public abstract void UpdateLocation(string chapter, string page);
        public abstract string GetTitle();
        public abstract string GetUserTitle();

        public static TitleType GetType(DirectoryInfo dir)
        {
            switch (File.ReadAllLines(FileHelper.GetFilePath(dir, "manga.txt"))[0])
            {
                case "manga":
                    return TitleType.MANGA;
                default:
                    return TitleType.NULL;
            }
        }
    }
}
