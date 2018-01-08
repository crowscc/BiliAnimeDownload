using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using BiliAnimeDownload.Droid;
using Android.Content.PM;

[assembly: Dependency(typeof(Utils))]
namespace BiliAnimeDownload.Droid
{
    public class Utils : IUtils
    {
        
        public void OpenUri(string url)
        {
            try
            {
                Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
                Android.App.Application.Context.StartActivity(browserIntent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, "打不开链接："+ url, ToastLength.Long).Show();
               
            }
           
        }


    }
}