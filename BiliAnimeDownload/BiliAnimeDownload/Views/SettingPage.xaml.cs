using BiliAnimeDownload.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiliAnimeDownload.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : TabbedPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<ISetting>().UpdatePath += SettingPage_UpdatePath;
            LoadSetting();
        }

        private void SettingPage_UpdatePath(object sender, EventArgs e)
        {
            
            txt_DownPath.Text = SettingHelper.GetDownPath();
        }

        bool loadSetting = true;
        private void LoadSetting()
        {
            loadSetting = true;
            txt_ver.Text = "Ver " + Util.GetVersion();


            cb_DownloadMode.SelectedIndex = SettingHelper.GetDownMode();
            txt_DownPath.Text = SettingHelper.GetDownPath();
            cb_Client.SelectedIndex = SettingHelper.GetClientMode();
            cb_Q.SelectedIndex = SettingHelper.GetQuality();
            loadSetting = false;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var lb = sender as Label;
            Util.OpenUri(lb.Text);
        }

        //...Xamarin找不到hyperlink控件...
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Util.OpenUri("https://qr.alipay.com/FKX06526G3SYZ8MZZE2Q77");
        }

        private void cb_DownloadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_DownloadMode.SelectedIndex==-1|| loadSetting)
            {
                return;
            }
            SettingHelper.SetDownMode(cb_DownloadMode.SelectedIndex);
        

        }

        private void cb_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Client.SelectedIndex == -1 || loadSetting)
            {
                return;
            }
            SettingHelper.SetClientMode(cb_Client.SelectedIndex);
        }

        private void cb_Q_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Q.SelectedIndex == -1 || loadSetting)
            {
                return;
            }
            SettingHelper.SetQuality(cb_Q.SelectedIndex);
        }

        private void btn_CheckUpdate_Clicked(object sender, EventArgs e)
        {
            Util.CheckUpdate(this, true);
        }

        private void btn_SelectPath_Clicked(object sender, EventArgs e)
        {
            Util.PickFolder();
           
        }

        private void btn_DefaultPath_Clicked(object sender, EventArgs e)
        {
            SettingHelper.SetDownPath("/storage/emulated/0/Android/data");
            txt_DownPath.Text = "/storage/emulated/0/Android/data";
        }
    }
}