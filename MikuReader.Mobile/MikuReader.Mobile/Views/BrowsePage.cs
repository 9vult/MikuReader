using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MikuReader.Mobile.Views
{
	public class BrowsePage : ContentPage
	{
		public BrowsePage ()
		{
            Title = "Browse";
			Content = new StackLayout {
				Children = {
					new Label { Text = "Browse Page" }
				}
			};
		}
	}
}