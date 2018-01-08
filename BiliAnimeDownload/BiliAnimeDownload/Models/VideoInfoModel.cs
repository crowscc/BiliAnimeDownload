using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload.Models
{
    class VideoInfoModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public VideoInfoModel data { get; set; }
        public string aid { get; set; }
        public string attribute { get; set; }
        public int copyright { get; set; }
        public long? ctime { get; set; }
        public string desc { get; set; }
        public long? duration { get; set; }//长度，秒
        public string pic { get; set; }
        public long? pubdate { get; set; }
        public int state { get; set; }
        public string title { get; set; }
        public int tid { get; set; }
        public string tname { get; set; }


        public List<pagesModel> pages { get; set; }

        public rightsModel rights { get; set; }



      
    }
    public class rightsModel
    {
        public int bp { get; set; }
        public int download { get; set; }
        public int elec { get; set; }
        public int hd5 { get; set; }
        public int movie { get; set; }
        public int pay { get; set; }
    }
    public class pagesModel
    {
        public long cid { get; set; }
        public string from { get; set; }
        public string has_alias { get; set; }
        public string link { get; set; }
        public int page { get; set; }
        public string part { get; set; }

        
        public string rich_vid { get; set; }
        public string vid { get; set; }
        public string weblink { get; set; }
      


    }
}
