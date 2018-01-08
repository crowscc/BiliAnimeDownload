using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiliAnimeDownload
{
    public interface IToast
    {
        void LongAlert(string message);
        void ShortAlert(string message);
        void OpenUri(string url);
        bool WriteFile(string content,string sid,string eid,string quality,int clientType,string urls);
        bool WriteFile(string content, string sid, string eid, string quality, int clientType, string urls, List<long> length, List<string> playUrls, string aid);
    }
    public class ShowToast
    {
        public static void ShowLongAlert(string message)
        {
            DependencyService.Get<IToast>().LongAlert(message);
        }
        public static void ShowShortAlert(string message)
        {
            DependencyService.Get<IToast>().ShortAlert(message);
        }
        public static void OpenUri(string url)
        {
            DependencyService.Get<IToast>().OpenUri(url);
        }
        public static bool ToWriteFile(string message,string sid, string eid,string quality, int clientType,string urls)
        {
            return DependencyService.Get<IToast>().WriteFile(message, sid,  eid, quality, clientType, urls);
        }
        public static bool ToWriteFile(string message, string sid, string eid, string quality, int clientType, string urls, List<long> length, List<string> playUrls, string aid)
        {
           
            return DependencyService.Get<IToast>().WriteFile(message, sid, eid, quality, clientType, urls, length, playUrls,  aid);
        }
    }


}
