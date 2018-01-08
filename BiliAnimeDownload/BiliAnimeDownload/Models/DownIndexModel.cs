using BiliAnime.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload.Models
{
    public class DownIndexModel
    {
        public string from { get; set; } = "bangumi";
        public string type_tag { get; set; } = "lua.hdflv2.bb2api.bd";

        public string description { get; set; } = "高清";
        public bool is_stub { get; set; } = false;
        public int psedo_bitrate { get; set; } = 0;

        public List<segment_listModel> segment_list { get; set; }

        public long parse_timestamp_milli { get; set; } = Api.GetTimeSpan_2;

        public long available_period_milli { get; set; } = 0;

        public int local_proxy_type { get; set; } = 0;

        public string user_agent { get; set; } = "Bilibili Freedoooooom/MarkII";
        public bool is_downloaded { get; set; } = false;
        public bool is_resolved { get; set; } = true;
        public List<player_codec_config_listModel> player_codec_config_list { get; set; }
        public long time_length { get; set; }

    }
    public class player_codec_config_listModel
    {
        public string player { get; set; }
        public bool use_list_player { get; set; } = false;
        public bool use_ijk_media_codec { get; set; } = false;
    }
    public class segment_listModel
    {
        public string url { get; set; }
        public long duration { get; set; } = 0;
        public long bytes { get; set; } = 0;
        public string meta_url { get; set; } = "";
    }
}
