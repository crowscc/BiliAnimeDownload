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
using Compass.FilePicker;

[assembly: Dependency(typeof(SettingHelper))]
namespace BiliAnimeDownload.Droid
{
    public class SettingHelper:ISetting
    {
        public event EventHandler UpdatePath;

        public string GetVersion()
        {
            PackageManager packageManager = Forms.Context.PackageManager;
            var packinfo = packageManager.GetPackageInfo(Forms.Context.PackageName, 0);
            return packinfo.VersionName;

        }

        public int GetVersioncode()
        {
            PackageManager packageManager = Forms.Context.PackageManager;
            var packinfo = packageManager.GetPackageInfo(Forms.Context.PackageName, 0);
            return packinfo.VersionCode;
        }
        public string GetSetting(string name)
        {
            try
            {
                ISharedPreferences sp = Forms.Context.GetSharedPreferences("setting", FileCreationMode.Private);
                string value = sp.GetString(name, "");

                return value;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Forms.Context, "为什么读个设置都会出错啊！\r\n"+ex.Message, ToastLength.Long);
                return "";
            }
          
        }

        public void SavaSetting(string name, string value)
        {
            try
            {
                ISharedPreferences sp = Forms.Context.GetSharedPreferences("setting", FileCreationMode.Append);
                ISharedPreferencesEditor editor = sp.Edit();
                editor.PutString(name, value);
                editor.Apply();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Forms.Context, "为什么保存个设置都会出错啊！\r\n" + ex.Message, ToastLength.Long);
            }

        }

        public void PickFolder()
        {
            try
            {
                FilePickerFragment filePickerFragment = new FilePickerFragment(null, null, FilePickerMode.Directory);
                filePickerFragment.FileSelected += (sender, path) =>
                {
                    filePickerFragment.Dismiss();
                   
                    SavaSetting("DownPath", path);
                    if (UpdatePath != null)
                    {
                        UpdatePath(this, null);
                    }
                };
                var activity = (Activity)Forms.Context;
                FragmentTransaction transaction = activity.FragmentManager.BeginTransaction();
                filePickerFragment.Cancel += sender => filePickerFragment.Dismiss();
                filePickerFragment.Show(transaction, "FilePicker");
              
            }
            catch (Exception ex)
            {
                Toast.MakeText(Forms.Context, "无法打开目录选择器！\r\n" + ex.Message, ToastLength.Long);

            }
           
        }


    }
}