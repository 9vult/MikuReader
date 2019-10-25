using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MikuReader.Mobile.Views
{
	public class ListPage : ContentPage
	{
        private List<string> titles;

        // Form children
        private ProgressBar pBar = new ProgressBar();
        //

		public ListPage ()
		{
            titles = new List<string>();

            FileHelper.APP_ROOT = FileHelper.CreateDI(Path.Combine(Environment.GetFolderPath(
                                            Environment.SpecialFolder.Personal), "MikuReader2"));
            if (!Directory.Exists(FileHelper.APP_ROOT.FullName)) Directory.CreateDirectory(FileHelper.APP_ROOT.FullName);

            // FORM SETUP
            this.Title = "List";
            this.Content = new StackLayout {
                Children = {
                    pBar,
                    new ListView { ItemsSource = titles }
                }
            };

            pBar.Progress = 50;

            RepopulateItems();

            // temp();
		}

        private void temp()
        {
            var url = "https://mangadex.org/api/manga/" + "32043";
            Manga m = new Manga(FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, "32043")), url);
            MClient.dbm.GetMangaDB().Add(m);
            foreach (Chapter c in m.GetChapters())
            {
                MClient.dlm.AddToQueue(new MangaDexDownload(c));
            }
            MClient.dlm.DownloadNext();

            RepopulateItems();
        }

        private void RepopulateItems()
        {
            titles.Clear();

            MClient.dbm.Refresh();
            foreach (Manga m in MClient.dbm.GetMangaPopulation())
            {
                string shortName = m.GetUserTitle();
                if (shortName.Length > 30)
                {
                    shortName = shortName.Substring(0, 30);
                }
                else
                {
                    shortName = shortName.PadRight(30);
                }
                titles.Add(shortName + " » c" + m.GetCurrentChapter() + ",p" + m.GetCurrentPage());
            }

            foreach (Hentai h in MClient.dbm.GetHentaiPopulation())
            {
                string shortName = h.GetUserTitle();
                if (shortName.Length > 30)
                {
                    shortName = shortName.Substring(0, 30);
                }
                else
                {
                    shortName = shortName.PadRight(30);
                }
                titles.Add(shortName + " » p" + h.GetCurrentPage());
            }
        }

	}
}