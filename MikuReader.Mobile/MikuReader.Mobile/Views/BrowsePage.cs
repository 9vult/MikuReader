using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MikuReader.Mobile.Views
{
	public class BrowsePage : ContentPage
	{
        WebView browser;
        Picker sitePicker;
        Button btnAdd;

		public BrowsePage ()
		{
            Title = "Browse";

            // SITE PICKER
            sitePicker = new Picker
            {
                Items = { "MangaDex", "nHentai" }
            };
            sitePicker.SelectedIndex = 0;
            sitePicker.SelectedIndexChanged += SitePicker_IndexChanged;

            // ADD BUTTON
            btnAdd = new Button
            {
                Text = "Add"
            };
            btnAdd.IsEnabled = false;
            btnAdd.Clicked += BtnAdd_Clicked;

            // BROWSER
            browser = new WebView
            {
                Source = "https://mangadex.org"
            };
            browser.Navigated += Browser_Navigated;
            
            Content = new StackLayout {
                Children = {
                    sitePicker,
                    btnAdd,
                    browser
				}
			};
		}

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void Browser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (browser.Source == null) return;

            switch (sitePicker.SelectedItem.ToString().ToLower())
            {
                case "mangadex":
                    if (browser.Source.ToString().StartsWith("https://mangadex.org/title/"))
                        btnAdd.IsEnabled = true;
                    else
                        btnAdd.IsEnabled = false;
                    break;
                case "nhentai":
                    if (browser.Source.ToString().StartsWith("https://nhentai.net/g/"))
                        btnAdd.IsEnabled = true;
                    else
                        btnAdd.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        private void SitePicker_IndexChanged(object sender, EventArgs e)
        {
            switch (sitePicker.SelectedItem.ToString())
            {
                case "MangaDex":
                    browser.Source = "https://mangadex.org";
                    break;
                case "nHentai":
                    browser.Source = "https://nhentai.net";
                    break;
            }
        }
    }
}