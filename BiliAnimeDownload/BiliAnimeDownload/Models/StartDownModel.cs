using BiliAnimeDownload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload
{
    public enum ClientType
    {
        release = 0,//正式版
        blue = 1,   //概念版
        white = 2,   //哔哩哔哩白
    }
    public enum VideoType
    {
        Anime = 0,//普通动漫
        AreaAnime = 1,//地区受限动漫
        Video = 2, //普通视频
        Movie = 3, //电影
        VipAnime=4,//大会员动漫
        VipMovie=5 //大会员电影
    }
    public enum DownlaodType
    {
        Bilibili = 0,
        System = 1
     
    }
    public class StartDownModel
    {
        public ClientType clientType { get; set; }
        public VideoType videoType { get; set; }
        public DownlaodType downlaodType { get; set; }
        public string downPath { get; set; } = "/storage/emulated/0/Android/data";
        public string season_id { get; set; }
        public string episode_id { get; set; }
        public string av_id { get; set; }
        public string danmaku_id { get; set; }
        public int page { get; set; }
        public string quality { get; set; }
        public string title { get; set; }
        public string entry_content { get; set; }//entry.json内容
        public string index_content { get; set; }//index.json内容
        public List<segment_listModel> urls { get; set; }
    }

    public class durlModel
    {
        public int code { get; set; } = 0;

        public string message { get; set; }
        public string format { get; set; }

        public long timelength { get; set; }

        public List<durlModel> durl { get; set; }
        public int order { get; set; }
        public long length { get; set; }
        public long size { get; set; }
        public string url { get; set; }
        public List<string> backup_url { get; set; }
    }

}
