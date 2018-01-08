using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BiliAnimeDownload
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Current.MainPage = new NavigationPage(new MainPage() {
                Title = "哔哩下载工具"

            });

            //MainPage = new BiliAnimeDownload.MainPage() {
            //     Title="Test",
                 
            //};
        }
        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
