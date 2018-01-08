using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload.Models
{
    public class BangumiInfoModel 
    {
        public int code { get; set; }
        public string message { get; set; }
        public BangumiInfoModel result { get; set; }

        public string alias { get; set; }//别名
        public string area { get; set; }//地区
        public string bangumi_title { get; set; }//番剧名。与season_title不同
        public string season_title { get; set; }//专题名
        public string title { get; set; }
        public string evaluate { get; set; }//介绍
        public int favorites { get; set; }//订阅
        public int coins { get; set; }//硬币
        public int play_count { get; set; }
        public int danmaku_count { get; set; }
        public int is_finish { get; set; }//是否完结
        public string newest_ep_index { get; set; }//最新话
        public string staff { get; set; }

        public string cover { get; set; }
        public DateTime? pub_time
        {
            get;
            set;
        }

        public BangumiInfoModel user_season { get; set; }
        public int attention { get; set; }//是否关注
        public string last_ep_index { get; set; }

        public int Num { get; set; }
        public List<tagsModel> tags { get; set; }



        public List<actorModel> actor { get; set; }
        public string role { get; set; }
        public List<episodesModel> episodes { get; set; }



        public int total_count { get; set; }
        public string status
        {
            get
            {
                if (is_finish == 1)
                {
                    return string.Format("已完结，共{0}话", total_count);
                }
                else
                {
                    return string.Format("连载中，更新至{0}话", newest_ep_index) ?? "";
                }
            }
        }

       
        public object rank { get; set; }
        public object list { get; set; }
        public List<BangumiInfoModel> seasons { get; set; }
        public string bangumi_id { get; set; }
        public string season_id { get; set; }
        public int season_type { get; set; }
        //public string title { get; set; }
        public int total_bp_count { get; set; }
        public int week_bp_count { get; set; }


       
    }

    public class FlvPlyaerUrlModel
    {
        public string format { get; set; }
        public int code { get; set; }
        public List<FlvPlyaerUrlModel> durl { get; set; }
        public int order { get; set; }
        public int length { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string[] backup_url { get; set; }

    }

    public class actorModel
    {
        public string actor { get; set; }
        public string role { get; set; }
    }
    public class tagsModel
    {
        public string index { get; set; }
        public int tag_id { get; set; }
        public string tag_name { get; set; }
    }
    public class episodesModel
    {
        //public string av_id { get; set; }
        public string aid { get; set; }
        public long cid { get; set; }
        public string ep_id { get; set; }
        //public int danmaku { get; set; }
        public string index_title { get; set; }
        public bool inLocal { get; set; }
        public string index { get; set; }
        public int orderindex { get; set; }
        public int episode_status { get; set; }
        //public string episode_id { get; set; }
        public string badge { get; set; }
        public string cover { get; set; }
        public string page { get; set; }
    }
}
