using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload
{
    public class EntryModel
    {
        public string season_id { get; set; }
        public string avid { get; set; }
        public string title { get; set; }
        public string cover { get; set; }

        public sourceModel source { get; set; }
        public epModel ep { get; set; }
        public page_dataModel page_data { get; set; }

        public bool is_completed { get; set; } = false;
        public long total_bytes { get; set; } = 0;
        public long downloaded_bytes { get; set; } = 0;

        public string type_tag { get; set; } = "lua.flv720.bb2api.64";

        public int prefered_video_quality { get; set; } = 100;
        public int guessed_total_bytes { get; set; } = 0;

        public long total_time_milli { get; set; } = 0;
        public int danmaku_count { get; set; } = 3000;
        public long time_update_stamp { get; set; } = 1511875136369;
        public long time_create_stamp { get; set; } = 1511868244605;
    }

    public class epModel
    {
        public long av_id { get; set; }
        public string page { get; set; }
        public long danmaku { get; set; }
        public string cover { get; set; }

        public long episode_id { get; set; }

        public string index { get; set; }
        public string index_title { get; set; }
    }
    public class sourceModel
    {
        public long av_id { get; set; }
        public long cid { get; set; }
        public string website { get; set; } = "bangumi";
        public string webvideo_id { get; set; }
    }

    public class page_dataModel
    {
        public long cid { get; set; }
        public int page { get; set; }
        public string from { get; set; } = "vupload";
        public string part { get; set; }
        public string link { get; set; } 
        public string weblink { get; set; }
        public string rich_vid { get; set; }
        public string offsite { get; set; }
        public string vid { get; set; }
        public bool has_alias { get; set; } = false;
        public int tid { get; set; }
    }




}
