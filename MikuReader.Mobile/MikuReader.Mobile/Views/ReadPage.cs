using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;

namespace MikuReader.Mobile.Views
{
	public class ReadPage : ContentPage
	{
		public ReadPage ()
		{
            Title = "Read";
            
            Content = new StackLayout {
				Children = {
					new Label { Text = "Read Page" }
				}
			};
		}
	}
}