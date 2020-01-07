using MikuReader.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MikuReader.Mobile.Views
{
	public class BrowsePage : ContentPage
	{
        Button btnAdd;

        Label lblInst;
        Label lblURL;
        Label lblName;

        Entry txtUrl;
        Entry txtName;

		public BrowsePage ()
		{
            Title = "Browse";

            // ADD BUTTON
            btnAdd = new Button
            {
                Text = "Add"
            };
            btnAdd.Clicked += BtnAdd_Clicked;

            // INSTRUCTIONS
            lblInst = new Label
            {
                Text = "Manga: Enter the MangaDex title URL and an optional title override.\n\n" +
                       "Hentai: Enter the nHentai title URL and the title, or leave blank for a random name."
            };

            // URL LABEL
            lblURL = new Label
            {
                Text = "URL:"
            };

            // NAME LABEL
            lblName = new Label
            {
                Text = "Title Override:"
            };

            txtName = new Entry();
            txtUrl = new Entry();

            Grid grid;
            Content = grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(4, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                }
            };

            grid.Children.Add(lblInst, 1, 0);
            grid.Children.Add(lblURL, 0, 1);
            grid.Children.Add(lblName, 0, 2);
            grid.Children.Add(txtUrl, 1, 1);
            grid.Children.Add(txtName, 1, 2);
            grid.Children.Add(btnAdd, 1, 3);
        }


        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            string url = txtUrl.Text;
            string name = string.Empty;
            string num = string.Empty;

            if (url == "" || url == null)
            {
                PopUp("Error", "Please enter a URL");
                return;
            }

            if (url.StartsWith("https://mangadex.org/title/"))
            {
                // TODO: Name
                name = url.Split('/')[5];
                num = url.Split('/')[4];
                url = "https://mangadex.org/api/manga/" + num;

                Manga m = new Manga(FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, num)), url);

                MClient.dbm.GetMangaDB().Add(m);

                foreach (Chapter c in m.GetChapters())
                {
                    MClient.dlm.AddToQueue(new MangaDexDownload(c));
                }
                // Start downloading the first one
                MClient.dlm.DownloadNext();
            }
            else if (url.StartsWith("https://nhentai.net/g/"))
            {
                num = url.Split('/')[4];
                name = txtName.Text != "" ? txtName.Text : "Hentai " + new Random().Next();

                JObject hJson = new JObject(
                    new JProperty("hentai",
                        new JObject(
                            new JProperty("title", name),
                            new JProperty("num", num),
                            new JProperty("url", url))));

                DirectoryInfo hDir = FileHelper.CreateDI(Path.Combine(FileHelper.APP_ROOT.FullName, "h" + num));

                Hentai h = new Hentai(hDir, hJson.ToString());
                MClient.dbm.GetMangaDB().Add(h);

                Chapter ch = h.GetChapters()[0];
                MClient.dlm.AddToQueue(new NhentaiDownload(ch));
                // Start downloading the first one
                MClient.dlm.DownloadNext();
            } else
            {
                PopUp("Error", "Invalid URL!");
                return;
            }
            PopUp("Info", "Download started!\nPlease do not close MikuReader until the download is complete.");
        }

        private async void PopUp(string title, string text)
        {
            await DisplayAlert(title, text, "OK");
        }

    }
}