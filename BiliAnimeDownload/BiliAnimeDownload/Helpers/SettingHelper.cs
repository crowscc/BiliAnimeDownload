using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload.Helpers
{
    public static class SettingHelper
    {
        public static int GetDownMode()
        {
            var DownMode = Util.GetSetting("DownMode");
            if (DownMode == "")
            {
               SetDownMode(0);
               return 0;
            }
            else
            {
                return Convert.ToInt32(DownMode);
            }
        }

        public static void SetDownMode(int value)
        {
            Util.SavaSetting("DownMode", value.ToString());
        }


        public static string GetDownPath()
        {
            var DownPath = Util.GetSetting("DownPath");
            if (DownPath == "")
            {
                SetDownPath("/storage/emulated/0/Android/data");
                return "/storage/emulated/0/Android/data";
            }
            else
            {
                return DownPath;
            }
        }

        public static void SetDownPath(string value)
        {
            Util.SavaSetting("DownPath", value);
        }


        public static int GetClientMode()
        {
            var ClientMode = Util.GetSetting("ClientMode");
            if (ClientMode == "")
            {
                SetClientMode(0);
                return 0;
            }
            else
            {
                return Convert.ToInt32(ClientMode);
            }
        }

        public static void SetClientMode(int value)
        {
            Util.SavaSetting("ClientMode", value.ToString());
        }



        public static int GetQuality()
        {
            var Quality = Util.GetSetting("Quality");
            if (Quality == "")
            {
                SetQuality(1);
                return 1;
            }
            else
            {
                return Convert.ToInt32(Quality);
            }
        }

        public static void SetQuality(int value)
        {
            Util.SavaSetting("Quality", value.ToString());
        }



    }
}
