using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using BiliAnimeDownload.Droid;
using Android.Content.PM;
using System.IO;

[assembly: Dependency(typeof(DownloadHelper))]
namespace BiliAnimeDownload.Droid
{
    public class DownloadHelper : IDownloadHelper
    {
        
        public MsgModel StartDownload(StartDownModel startDownModel)
        {
            switch (startDownModel.videoType)
            {
                case VideoType.Anime:
                    if (startDownModel.downlaodType== DownlaodType.Bilibili)
                    {
                        return ClientDownloadAnime(startDownModel);
                    }
                    else
                    {
                        return SystemDownloadAnime(startDownModel);
                    }
                case VideoType.AreaAnime:
                    return SystemDownloadAnime(startDownModel);
                case VideoType.Video:
                case VideoType.Movie:
                case VideoType.VipMovie:
                    if (startDownModel.downlaodType == DownlaodType.Bilibili)
                    {
                        return ClientDownloadVideo(startDownModel);
                    }
                    else
                    {
                        return SystemDownloadVideo(startDownModel);
                    }
                default:
                    return new MsgModel() { message = "不支持此类型的视频下载" };
            }
        }

        /*目录结构
         * /哔哩哔哩下载目录
         *      番剧
         *          剧集
         *              视频目录
         *                  index.json
         *                  0.blv
         *                  0.0.blv.4m.sum
         *              entry.json
         *              danmaku.xml
         */
        private MsgModel ClientDownloadAnime(StartDownModel startDownModel)
        {
            try
            {

                var biliPath = startDownModel.downPath + "/tv.danmaku.bili/download/";
                if (startDownModel.clientType == ClientType.blue)
                {
                    biliPath = startDownModel.downPath + "/com.bilibili.app.blue/download/";
                }
                Java.IO.File bilidirv = new Java.IO.File(biliPath);
                if (!bilidirv.Exists())
                {
                    bilidirv.Mkdirs();
                    //return new MsgModel() { message = "请检查你是否安装了哔哩哔哩客户端或者是否更改了哔哩哔哩客户端的下载目录" };
                }

                //番剧目录
                string path = biliPath + "s_" + startDownModel.season_id + "/";
                Java.IO.File dirv = CreateFolder(path);

                //剧集目录
                string e_path = path + startDownModel.episode_id + "/";
                Java.IO.File e_path_dirv = CreateFolder(e_path);

                //剧集entry.json文件
                string json_path = e_path + "/entry.json";
                Java.IO.File json_path_file = CreateFile(json_path);
                WriteFileContent(json_path, startDownModel.entry_content);
                //剧集弹幕文件
                string danmu_path = e_path + "/danmaku.xml";
                Java.IO.File danmu_path_file = CreateFile(danmu_path);
               

                //剧集视频文件夹
                string video_path = e_path + "/" + startDownModel.quality + "/";
                Java.IO.File video_path_dirv =  CreateFolder(video_path);

                //剧集视频index.json
                string indexjson_path = video_path + "index.json";
                Java.IO.File indexjson_path_file = CreateFile(indexjson_path);
                WriteFileContent(indexjson_path, startDownModel.index_content);


                return new MsgModel() {code=200, message = "任务创建成功，请打开哔哩哔哩客户端查看" };
            }
            catch (Exception ex)
            {
                return new MsgModel() { code = ex.HResult, message = "任务创建失败，出现了未知错误\r\n"+ex.Message };
            }
        }
        private MsgModel SystemDownloadAnime(StartDownModel startDownModel)
        {
            try
            {
                var biliPath = startDownModel.downPath + "/tv.danmaku.bili/download/";
                if (startDownModel.clientType == ClientType.blue)
                {
                    biliPath = startDownModel.downPath + "/com.bilibili.app.blue/download/";
                }
                Java.IO.File bilidirv = new Java.IO.File(biliPath);
                if (!bilidirv.Exists())
                {
                    bilidirv.Mkdirs();
                    //return new MsgModel() { message = "请检查你是否安装了哔哩哔哩客户端或者是否更改了哔哩哔哩客户端的下载目录" };
                }
                //番剧目录
                string path = biliPath + "s_" + startDownModel.season_id + "/";
                Java.IO.File dirv = CreateFolder(path);

                //剧集目录
                string e_path = path + startDownModel.episode_id + "/";
                Java.IO.File e_path_dirv = CreateFolder(e_path);

                //剧集entry.json文件
                string json_path = e_path + "/entry.json";
                Java.IO.File json_path_file = CreateFile(json_path);
                WriteFileContent(json_path, startDownModel.entry_content);
                //剧集弹幕文件
                string danmu_path = e_path + "/danmaku.xml";
                Java.IO.File danmu_path_file = CreateFile(danmu_path);


                //剧集视频文件夹
                string video_path = e_path + "/" + startDownModel.quality + "/";
                Java.IO.File video_path_dirv = CreateFolder(video_path);

                //剧集视频index.json
                string indexjson_path = video_path + "index.json";
                Java.IO.File indexjson_path_file = CreateFile(indexjson_path);
                WriteFileContent(indexjson_path, startDownModel.index_content);

                for (int i = 0; i < startDownModel.urls.Count; i++)
                {
                    string sum_path = video_path + i + ".blv.4m.sum";
                    Java.IO.File sum_path_file = CreateFile(sum_path);
                    WriteFileContent(sum_path, "{\"length\":" + startDownModel.urls[i].bytes + "}");

                    string blv_path = video_path + i + ".blv";
                    Java.IO.File blv_path_file = new Java.IO.File(blv_path);

                    var download_id = CreateSystemDownloadTask(startDownModel.urls[i].url, blv_path_file, "https://www.bilibili.com/bangumi/play/ep"+ startDownModel.episode_id, startDownModel.title+i);

                }


                return new MsgModel() { code = 200, message = "任务创建成功，请等待下载完成打开哔哩哔哩客户端观看" };
            }
            catch (Exception ex)
            {
                return new MsgModel() { code = ex.HResult, message = "任务创建失败，出现了未处理错误\r\n" + ex.Message };
            }
        }
        private MsgModel ClientDownloadVideo(StartDownModel startDownModel)
        {
            try
            {
               
                var biliPath = startDownModel.downPath+"/tv.danmaku.bili/download/";
                if (startDownModel.clientType == ClientType.blue)
                {
                    biliPath = startDownModel.downPath + "/com.bilibili.app.blue/download/";
                }
                Java.IO.File bilidirv = new Java.IO.File(biliPath);
                if (!bilidirv.Exists())
                {
                    bilidirv.Mkdirs();
                    //return new MsgModel() { message = "请检查你是否安装了哔哩哔哩客户端或者是否更改了哔哩哔哩客户端的下载目录" };
                }
                
                //视频目录
                string path = biliPath +   startDownModel.av_id + "/";
                Java.IO.File dirv = CreateFolder(path);

                //剧集目录
                string e_path = path + startDownModel.page + "/";
                Java.IO.File e_path_dirv = CreateFolder(e_path);

                //剧集entry.json文件
                string json_path = e_path + "/entry.json";
                Java.IO.File json_path_file = CreateFile(json_path);
                WriteFileContent(json_path, startDownModel.entry_content);
                //剧集弹幕文件
                string danmu_path = e_path + "/danmaku.xml";
                Java.IO.File danmu_path_file = CreateFile(danmu_path);


                //剧集视频文件夹
                string video_path = e_path + "/" + startDownModel.quality + "/";
                Java.IO.File video_path_dirv = CreateFolder(video_path);

                //剧集视频index.json
                string indexjson_path = video_path + "index.json";
                Java.IO.File indexjson_path_file = CreateFile(indexjson_path);
                WriteFileContent(indexjson_path, startDownModel.index_content);


                return new MsgModel() { code = 200, message = "任务创建成功，请打开哔哩哔哩客户端查看" };
            }
            catch (Exception ex)
            {
                return new MsgModel() { code = ex.HResult, message = "任务创建失败，出现了未知错误\r\n" + ex.Message };
            }

        }
        private MsgModel SystemDownloadVideo(StartDownModel startDownModel)
        {
            try
            {
                var biliPath = startDownModel.downPath + "/tv.danmaku.bili/download/";
                if (startDownModel.clientType == ClientType.blue)
                {
                    biliPath = startDownModel.downPath + "/com.bilibili.app.blue/download/";
                }
                Java.IO.File bilidirv = new Java.IO.File(biliPath);
                if (!bilidirv.Exists())
                {
                    bilidirv.Mkdirs();
                    //return new MsgModel() { message = "请检查你是否安装了哔哩哔哩客户端或者是否更改了哔哩哔哩客户端的下载目录" };
                }
                //视频目录
                string path = biliPath + startDownModel.av_id + "/";
                Java.IO.File dirv = CreateFolder(path);

                //剧集目录
                string e_path = path + startDownModel.page + "/";
                Java.IO.File e_path_dirv = CreateFolder(e_path);

                //剧集entry.json文件
                string json_path = e_path + "/entry.json";
                Java.IO.File json_path_file = CreateFile(json_path);
                WriteFileContent(json_path, startDownModel.entry_content);
                //剧集弹幕文件
                string danmu_path = e_path + "/danmaku.xml";
                Java.IO.File danmu_path_file = CreateFile(danmu_path);


                //剧集视频文件夹
                string video_path = e_path + "/" + startDownModel.quality + "/";
                Java.IO.File video_path_dirv = CreateFolder(video_path);

                //剧集视频index.json
                string indexjson_path = video_path + "index.json";
                Java.IO.File indexjson_path_file = CreateFile(indexjson_path);
                WriteFileContent(indexjson_path, startDownModel.index_content);

                for (int i = 0; i < startDownModel.urls.Count; i++)
                {
                    string sum_path = video_path + i + ".blv.4m.sum";
                    Java.IO.File sum_path_file = CreateFile(sum_path);
                    WriteFileContent(sum_path, "{\"length\":" + startDownModel.urls[i].bytes + "}");

                    string blv_path = video_path + i + ".blv";
                    Java.IO.File blv_path_file = new Java.IO.File(blv_path);

                    var download_id = CreateSystemDownloadTask(startDownModel.urls[i].url, blv_path_file, "https://www.bilibili.com/video/av" + startDownModel.av_id, startDownModel.title + i);

                }


                return new MsgModel() { code = 200, message = "任务创建成功，请等待下载完成打开哔哩哔哩客户端观看" };
            }
            catch (Exception ex)
            {
                return new MsgModel() { code = ex.HResult, message = "任务创建失败，出现了未处理错误\r\n" + ex.Message };
            }
        }

        private Java.IO.File CreateFile(string path)
        {
            Java.IO.File file = new Java.IO.File(path);
            if (!file.Exists())
            {
                file.CreateNewFile();
            }
            return file;
        }
        

        private void WriteFileContent(string path,string content)
        {
            FileStream fileOS = new FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            Java.IO.BufferedWriter buf1 = new Java.IO.BufferedWriter(new Java.IO.OutputStreamWriter(fileOS));
            buf1.Write(content, 0, content.Length);
            buf1.Flush();
            buf1.Close();

        }

        private Java.IO.File CreateFolder(string path)
        {
            Java.IO.File _dirv = new Java.IO.File(path);
            if (!_dirv.Exists())
            {
                _dirv.Mkdirs();
            }
            return _dirv;
        }

        private long CreateSystemDownloadTask(string url, Java.IO.File saveFile, string Referer, string title = "")
        {
            try
            {

                DownloadManager downloadManager = (DownloadManager)Forms.Context.GetSystemService(Context.DownloadService);
                DownloadManager.Request request = new DownloadManager.Request(Android.Net.Uri.Parse(url));
                if (title.Length != 0)
                {
                    request.SetTitle(title);
                }

                request.AddRequestHeader("Referer", Referer);
                request.AddRequestHeader("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");
                request.SetDestinationUri(Android.Net.Uri.FromFile(saveFile));
                request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
                long downloadId = downloadManager.Enqueue(request);
                return downloadId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}