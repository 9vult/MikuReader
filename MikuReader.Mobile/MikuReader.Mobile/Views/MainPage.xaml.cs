using MikuReader.Core;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MikuReader.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            FileHelper.APP_ROOT = FileHelper.CreateDI(Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), "MikuReader2")); // TODO

        }
    }
}