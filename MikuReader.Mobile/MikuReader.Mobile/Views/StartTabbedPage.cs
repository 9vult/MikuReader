using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MikuReader.Mobile.Views
{
	public class StartTabbedPage : TabbedPage
	{
		public StartTabbedPage ()
        {
            var listPage = new ListPage();
            var browsePage = new BrowsePage();
            var readPage = new ReadPage();

            Children.Add(listPage);
            Children.Add(readPage);
            Children.Add(browsePage);
		}
	}
}