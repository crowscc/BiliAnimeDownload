using BiliAnime.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Flurl.Http;
using Android.App;
using BiliAnimeDownload.Models;
using BiliAnimeDownload.Helpers;
using BiliAnimeDownload.Views;
using Newtonsoft.Json.Linq;

namespace BiliAnimeDownload
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            if (BindingContext == null)
            {

                await Task.Delay(500);

                //txt_Sid.Text = "http://m.bilibili.com/bangumi/play/ss6339";
                //txt_Sid.Text = "av17254855";

                //txt_Sid.Text = "http://m.bilibili.com/bangumi/play/ss4187";
                Util.CheckUpdate(this, false);
            }
            _downlaodType =(DownlaodType) SettingHelper.GetDownMode();

        }

        VideoType _videoType = VideoType.Anime;
        DownlaodType _downlaodType = DownlaodType.Bilibili;
        string _sid = "";
        string _aid = "";
        private void btn_Go_Clicked(object sender, EventArgs e)
        {
            if (txt_Sid.Text.Length == 0)
            {
                Util.ShowShortToast("请输入番剧地址");
                return;
            }
            if (txt_Sid.Text.Contains("av"))
            {
                _aid = Regex.Match(txt_Sid.Text, @"\d{1,9}", RegexOptions.Singleline).Groups[0].Value;
                _videoType = VideoType.Video;
                BindingContext = null;
                ls.IsVisible = false;
                ls_Video.IsVisible = true;
                LoadVideoInfo();
            }
            else
            {
                _sid = Regex.Match(txt_Sid.Text, @"\d{1,9}", RegexOptions.Singleline).Groups[0].Value;
                _videoType = VideoType.Anime;
                BindingContext = null;
                ls.IsVisible = true;
                ls_Video.IsVisible = false;
                LoadAnimeInfo();
            }
            _downlaodType = (DownlaodType)SettingHelper.GetDownMode();

        }


        private async void LoadAnimeInfo()
        {
            try
            {

                Loading.IsVisible = true;

                //var str = await Api._BanInfoApi(_sid).GetStringAsync();
                var eresults = await Api._BanInfoApi2(_sid).GetStringAsync();
                // str1 = str1.Replace("seasonListCallback(", "");
                // str1 = str1.Remove(str1.Length-2,2);


                BangumiInfoModel model = JsonConvert.DeserializeObject<BangumiInfoModel>(eresults);

                if (model.code == 0)
                {
                    BindingContext = model.result;

                    if (model.result.title.Contains("僅"))
                    {
                        _videoType = VideoType.AreaAnime;
                        await DisplayAlert("说明", "你下载的是地区专供番,将会调用系统下载", "知道了");
                    }
                }
                else
                {
                    Util.ShowShortToast("加载失败,请检查网址是否正确");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("读取失败", ex.Message, "OK");
            }
            finally
            {
                Loading.IsVisible = false;
            }

        }


        private async void LoadVideoInfo()
        {
            try
            {

                Loading.IsVisible = true;

                //var str = await Api._BanInfoApi(_sid).GetStringAsync();
                var eresults = await Api._VideoInfoApi(_aid).GetStringAsync();
                // str1 = str1.Replace("seasonListCallback(", "");
                // str1 = str1.Remove(str1.Length-2,2);


                VideoInfoModel model = JsonConvert.DeserializeObject<VideoInfoModel>(eresults);
                if (model.code == 0)
                {
                    BindingContext = model.data;

                    if (model.data.rights.movie == 1)
                    {
                        _videoType = VideoType.Movie;
                        if (model.data.rights.pay == 1)
                        {
                            _videoType = VideoType.VipMovie;
                        }
                    }
                }
                else
                {
                    Util.ShowShortToast("加载失败,请检查网址是否正确");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("读取失败", ex.Message, "OK");
            }
            finally
            {
                Loading.IsVisible = false;
            }

        }


        private void ls_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            switch (_videoType)
            {
                case VideoType.Anime:
                case VideoType.AreaAnime:
                    StartAnimeDownload(((ListView)sender).SelectedItem);
                    break;
                case VideoType.Video:
                case VideoType.Movie:
                case VideoType.VipMovie:
                    StartVideoDownload(((ListView)sender).SelectedItem);
                    break;
                case VideoType.VipAnime:
                    //这个先不用管他
                    break;
                default:
                    break;
            }
            ((ListView)sender).SelectedItem = null;
        }


        private async void StartAnimeDownload(object par)
        {
            try
            {
                var item = par as episodesModel;
                var data = BindingContext as BangumiInfoModel;
                List<segment_listModel> segment_list = new List<segment_listModel>();
                var indexJsonContent = "";
                var entryJsonContent = "";
                var description = "超清";

                int q = 3;
                //转换下清晰度
                var quality = "lua.flv720.bb2api.64";
                switch (SettingHelper.GetQuality())
                {
                    case 0:
                        quality = "lua.hdflv2.bb2api.bd";
                        description = "1080P";
                        q = 4;
                        break;
                    case 1:
                        quality = "lua.flv.bb2api.80";
                        description = "超清";
                        q = 3;
                        break;
                    case 2:
                        quality = "lua.flv720.bb2api.64";
                        description = "高清";
                        q = 2;
                        break;
                    case 3:
                        quality = "lua.mp4.bb2api.16";
                        description = "清晰";
                        q = 1;
                        break;
                    default:
                        break;
                }


                EntryModel entryModel = new EntryModel()
                {
                    season_id = data.season_id,
                    title = data.title,
                    cover = data.cover,
                    type_tag = quality,
                    time_create_stamp = Api.GetTimeSpan_2,
                    time_update_stamp = Api.GetTimeSpan_2,
                    source = new sourceModel()
                    {
                        av_id = long.Parse(item.aid),
                        cid = item.cid,
                        website = "bangumi",
                        webvideo_id = ""
                    },
                    ep = new epModel()
                    {
                        av_id = long.Parse(item.aid),
                        danmaku = item.cid,
                        cover = item.cover,
                        episode_id = long.Parse(item.ep_id),
                        index = item.index,
                        index_title = item.index_title,
                        page = item.page
                    }
                };
                //调用系统时将任务设置未完成
                if (_downlaodType == DownlaodType.System)
                {
                    entryModel.is_completed = true;
                }
                else
                {
                    entryModel.is_completed = false;
                }
                //当调用系统下载时才读取地址
                if (_downlaodType == DownlaodType.System)
                {

                    segment_list = await Util.GetVideoUrl(entryModel.ep.danmaku.ToString(), "https://www.bilibili.com/bangumi/play/ep" + entryModel.ep.episode_id, q);
                    long _tbyte = 0;
                    long _timelength = 0;
                    foreach (var item1 in segment_list)
                    {
                        _tbyte += item1.bytes;
                        _timelength += item1.duration;
                    }
                    DownIndexModel downUrlModel = new DownIndexModel()
                    {
                        type_tag = quality,
                        description = description,
                        parse_timestamp_milli = Api.GetTimeSpan_2,
                        time_length = _timelength
                    };

                    downUrlModel.segment_list = segment_list;
                    List<player_codec_config_listModel> player_Codec_Config_ListModel = new List<player_codec_config_listModel>() {
                            new  player_codec_config_listModel(){ player="IJK_PLAYER" },
                            new player_codec_config_listModel(){player="ANDROID_PLAYER"}};
                    downUrlModel.player_codec_config_list = player_Codec_Config_ListModel;
                    indexJsonContent = JsonConvert.SerializeObject(downUrlModel);
                    entryModel.total_time_milli = downUrlModel.time_length;
                    entryModel.total_bytes = _tbyte;
                }


                //序列化entryModel
                entryJsonContent = JsonConvert.SerializeObject(entryModel);


                //开始一个下载任务
                StartDownModel startDownModel = new StartDownModel()
                {
                    downPath=SettingHelper.GetDownPath(),
                    entry_content = entryJsonContent,
                    av_id = entryModel.ep.av_id.ToString(),
                    danmaku_id = entryModel.ep.danmaku.ToString(),
                    episode_id = entryModel.ep.episode_id.ToString(),
                    index_content = indexJsonContent,
                    quality = quality,
                    season_id = _sid,
                    title = entryModel.ep.index + entryModel.ep.index_title,
                    urls = segment_list,
                    clientType = (ClientType)SettingHelper.GetClientMode(),
                    downlaodType = _downlaodType,
                    videoType = _videoType
                };
                var msg = Util.StartDownload(startDownModel);
                if (msg.code == 200)
                {
                    Util.ShowShortToast(msg.message);
                }
                else
                {
                    await DisplayAlert("创建任务失败", msg.message, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("好气啊，出错了", ex.Message, "OK");
            }

        }
        private async void StartVideoDownload(object par)
        {
            try
            {
                var item = par as pagesModel;
                var data = BindingContext as VideoInfoModel;
                List<segment_listModel> segment_list = new List<segment_listModel>();
                var indexJsonContent = "";
                var entryJsonContent = "";
                var description = "超清";

                int q = 3;
                //转换下清晰度
                var quality = "lua.flv720.bb2api.64";
                switch (SettingHelper.GetQuality())
                {
                    case 0:
                        quality = "lua.hdflv2.bb2api.bd";
                        description = "1080P";
                        q = 4;
                        break;
                    case 1:
                        quality = "lua.flv.bb2api.80";
                        description = "超清";
                        q = 3;
                        break;
                    case 2:
                        quality = "lua.flv720.bb2api.64";
                        description = "高清";
                        q = 2;
                        break;
                    case 3:
                        quality = "lua.mp4.bb2api.16";
                        description = "清晰";
                        q = 1;
                        break;
                    default:
                        break;
                }


                EntryModel entryModel = new EntryModel()
                {
                    avid = _aid,
                    title = data.title,
                    cover = data.pic,
                    type_tag = quality,
                    time_create_stamp = Api.GetTimeSpan_2,
                    time_update_stamp = Api.GetTimeSpan_2,
                    page_data = new page_dataModel()
                    {
                        cid = item.cid,
                        from = item.from,
                        part = item.part,
                        vid = item.vid,
                        tid = data.tid,
                        page =item.page
                    }
                };
                //调用系统下载时将任务设置未完成
                if (_downlaodType == DownlaodType.System)
                {
                    entryModel.is_completed = true;
                }
                else
                {
                    entryModel.is_completed = false;
                }
                //当调用系统下载时才读取地址
                if (_downlaodType == DownlaodType.System)
                {
                    segment_list = await Util.GetVideoUrl(item.cid.ToString(), "https://www.bilibili.com/video/av16111678", q);
                    long _tbyte = 0;
                    long _timelength = 0;
                    foreach (var item1 in segment_list)
                    {
                        _tbyte += item1.bytes;
                        _timelength += item1.duration;
                    }
                    DownIndexModel downUrlModel = new DownIndexModel()
                    {
                        type_tag = quality,
                        description = description,
                        parse_timestamp_milli = Api.GetTimeSpan_2,
                        time_length = _timelength
                    };

                    downUrlModel.segment_list = segment_list;
                    List<player_codec_config_listModel> player_Codec_Config_ListModel = new List<player_codec_config_listModel>() {
                            new  player_codec_config_listModel(){ player="IJK_PLAYER" },
                            new player_codec_config_listModel(){player="ANDROID_PLAYER"}};
                    downUrlModel.player_codec_config_list = player_Codec_Config_ListModel;
                    indexJsonContent = JsonConvert.SerializeObject(downUrlModel);
                    entryModel.total_time_milli = downUrlModel.time_length;
                    entryModel.total_bytes = _tbyte;
                }


                //序列化entryModel
                entryJsonContent = JsonConvert.SerializeObject(entryModel);


                //开始一个下载任务
                StartDownModel startDownModel = new StartDownModel()
                {
                    downPath = SettingHelper.GetDownPath(),
                    entry_content = entryJsonContent,
                    av_id = _aid,
                    danmaku_id = item.cid.ToString(),
                    episode_id = "",
                    index_content = indexJsonContent,
                    quality = quality,
                    season_id = "",
                    title = item.page+" "+item.part,
                    urls = segment_list,
                    page=item.page,
                    clientType = (ClientType)SettingHelper.GetClientMode(),
                    downlaodType = _downlaodType,
                    videoType = _videoType
                };
                var msg = Util.StartDownload(startDownModel);
                if (msg.code == 200)
                {
                    Util.ShowShortToast(msg.message);
                }
                else
                {
                    await DisplayAlert("创建任务失败", msg.message, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("好气啊，出错了", ex.Message, "OK");
            }

        }
      

        private void menu_setting_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new SettingPage());
        }

        private void menu_help_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new HelpPage());
        }
    }









}
