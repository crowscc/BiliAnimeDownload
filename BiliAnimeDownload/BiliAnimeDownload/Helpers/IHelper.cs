using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliAnimeDownload
{
    /// <summary>
    /// 定义接口，Xamarin.Form与特定平台交互的方式
    /// https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/
    /// </summary>
    public interface IShowToast
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }

    public interface IUtils
    {
        void OpenUri(string url);
       
    }

    public interface IDownloadHelper
    {
        MsgModel StartDownload(StartDownModel  startDownModel);
    }

    public interface ISetting
    {
        void SavaSetting(string name,string value);
        string GetSetting(string name);
        int GetVersioncode();
        string GetVersion();

        void PickFolder();

        event EventHandler UpdatePath;

    }

}
