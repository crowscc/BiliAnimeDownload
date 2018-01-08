using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnime.Helpers
{
    public static class Api
    {
        public const string _appSecret = "560c52ccd288fed045859ed18bffd973";
        public const string _appKey = "1d8b6e7d45233436";
        public const string _appSecret_VIP = "9b288147e5474dd2aa67085f716c560d";
        public const string _appSecret_PlayUrl = "1c15888dc316e05a15fdd0a02ed6584f";
        public static string GetSign(string url)
        {
            string result;
            string str = url.Substring(url.IndexOf("?", 4) + 1);
            List<string> list = str.Split('&').ToList();
            list.Sort();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str1 in list)
            {
                stringBuilder.Append((stringBuilder.Length > 0 ? "&" : string.Empty));
                stringBuilder.Append(str1);
            }
            stringBuilder.Append(_appSecret);
            result = MD5.GetMd5String(stringBuilder.ToString()).ToLower();
            return result;
        }
        public static string GetSign_VIP(string url)
        {
            string result;
            string str = url.Substring(url.IndexOf("?", 4) + 1);
            List<string> list = str.Split('&').ToList();
            list.Sort();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str1 in list)
            {
                stringBuilder.Append((stringBuilder.Length > 0 ? "&" : string.Empty));
                stringBuilder.Append(str1);
            }
            stringBuilder.Append(_appSecret_VIP);
            result = MD5.GetMd5String(stringBuilder.ToString()).ToLower();
            return result;
        }

        public static string GetSign_PlayUrl(string url)
        {
            string result;
            string str = url.Substring(url.IndexOf("?", 4) + 1);
            List<string> list = str.Split('&').ToList();
            list.Sort();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str1 in list)
            {
                stringBuilder.Append((stringBuilder.Length > 0 ? "&" : string.Empty));
                stringBuilder.Append(str1);
            }
            stringBuilder.Append(_appSecret_PlayUrl);
            result = MD5.GetMd5String(stringBuilder.ToString()).ToLower();
            return result;
        }


        public static long GetTimeSpan
        {
            get { return Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds); }
        }
        public static long GetTimeSpan_2
        {
            get { return Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalMilliseconds); }
        }


        public static string _BanInfoApi(string sid)
        {

            string uri = string.Format("http://bangumi.bilibili.com/api/season_v3?_device=android&_ulv=10000&build=411005&platform=android&appkey=1d8b6e7d45233436&ts={0}000&type=bangumi&season_id={1}", GetTimeSpan, sid);
            uri += "&sign=" + GetSign(uri);
            return uri;

        }

        public static string _VideoInfoApi(string aid)
        {
            //这个API不用sign也可以访问，以防万一，还是加上...
            string uri = string.Format("http://app.bilibili.com/x/view?aid={0}&appkey=1d8b6e7d45233436&build=521000&ts={1}", aid, GetTimeSpan);
            uri += "&sign=" + GetSign(uri);
            return uri;

        }


        public static string _BanInfoApiJSONP(string sid)
        {

            string uri = string.Format("http://bangumi.bilibili.com/jsonp/seasoninfo/{0}.ver?callback=seasonListCallback&jsonp=jsonp&_={1}", sid, GetTimeSpan);
            //https://bangumi.bilibili.com/view/web_api/season?season_id=4187
            return uri;

        }
        public static string _BanInfoApi2(string sid)
        {

            string uri = string.Format("https://bangumi.bilibili.com/view/web_api/season?season_id={0}", sid);
            //
            return uri;

        }

        public static string _commentApi(string aid)
        {

            string uri = string.Format("https://api.bilibili.com/x/v2/reply?access_key=&appkey={1}&build=511000&mobi_app=android&oid={2}&plat=2&platform=android&pn=1&ps=20&sort=0&ts={0}&type=1", GetTimeSpan, _appKey, aid);
            uri += "&sign=" + GetSign(uri);
            return uri;

        }

        public static string _playurlApi(string cid, int quality=4)
        {


            var qn = 80;
            switch (quality)
            {
                case 1:
                    qn = 32;
                    break;
                case 2:
                    qn = 64;
                    break;
                case 3:
                    qn = 80;
                    break;
                case 4:
                    qn = 112;
                    break;
                default:
                    break;
            }

            string url = string.Format("https://interface.bilibili.com/playurl?cid={0}&player=1&quality={1}&qn={1}&ts={2}", cid, qn, GetTimeSpan);
            url += "&sign=" + GetSign_PlayUrl(url);

            return url;
        }

        public static string _playurlApi2(string cid, int quality = 4)
        {



            string url = string.Format("https://bangumi.bilibili.com/player/web_api/playurl/?cid={0}&module=bangumi&player=1&otype=json&type=flv&quality={1}&ts={2}", cid, quality, GetTimeSpan_2);
            url += "&sign=" + GetSign_VIP(url);

            return url;
        }


    }
}
