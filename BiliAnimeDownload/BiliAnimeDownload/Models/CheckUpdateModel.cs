using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload.Models
{
    public class CheckUpdateModel
    {
        public int versionCode { get; set; }
        public string version { get; set; }
        public string versionMessage { get; set; }
        public string updateUrl { get; set; }
    }
}
