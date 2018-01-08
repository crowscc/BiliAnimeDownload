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
using BiliAnimeDownload.Droid;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(Toast_Android))]
namespace BiliAnimeDownload.Droid
{
    public class Toast_Android : IToast
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
        public void OpenUri(string url)
        {
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            Android.App.Application.Context.StartActivity(browserIntent);
        }
        public bool WriteFile(string content, string sid, string eid, string quality, int clientType, string urls)
        {

            //Toast.MakeText(Android.App.Application.Context, Android.OS.Environment.DataDirectory.Path, ToastLength.Long).Show();
            //Android.OS.Environment.DataDirectory


            try
            {

                var biliPath = "/storage/emulated/0/Android/data/tv.danmaku.bili/download/";
                if (clientType == 1)
                {
                    biliPath = "/storage/emulated/0/Android/data/com.bilibili.app.blue/download/";
                }
                Java.IO.File bilidirv = new Java.IO.File(biliPath);
                if (!bilidirv.Exists())
                {
                    return false;
                }



                string path = biliPath + "s_" + sid + "/";
                Java.IO.File dirv = new Java.IO.File(path);
                if (!dirv.Exists())
                {
                    dirv.Mkdirs();
                }

                string e_path = path + eid + "/";
                Java.IO.File e_path_dirv = new Java.IO.File(e_path);
                if (!e_path_dirv.Exists())
                {
                    e_path_dirv.Mkdirs();
                }
                string json_path = e_path + "/entry.json";
                Java.IO.File json_path_file = new Java.IO.File(json_path);
                if (!json_path_file.Exists())
                {
                    json_path_file.CreateNewFile();
                }

                FileStream fileOS = new FileStream(json_path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                Java.IO.BufferedWriter buf1 = new Java.IO.BufferedWriter(new Java.IO.OutputStreamWriter(fileOS));
                buf1.Write(content, 0, content.Length);
                buf1.Flush();
                buf1.Close();


                string video_path = e_path + "/" + quality + "/";
                Java.IO.File video_path_dirv = new Java.IO.File(video_path);
                if (!video_path_dirv.Exists())
                {
                    video_path_dirv.Mkdirs();
                }

                if (urls.Length != 0)
                {
                    string indexjson_path = video_path + "index.json";
                    Java.IO.File indexjson_path_file = new Java.IO.File(indexjson_path);
                    if (!indexjson_path_file.Exists())
                    {
                        indexjson_path_file.CreateNewFile();
                    }
                    using (FileStream indexfile = new FileStream(indexjson_path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                    {
                        Java.IO.BufferedWriter buf2 = new Java.IO.BufferedWriter(new Java.IO.OutputStreamWriter(indexfile));
                        buf2.Write(urls, 0, urls.Length);
                        buf2.Flush();
                        buf2.Close();
                    }




                }



                string danmu_path = e_path + "/danmaku.xml";
                Java.IO.File danmu_path_file = new Java.IO.File(danmu_path);
                if (!danmu_path_file.Exists())
                {
                    danmu_path_file.CreateNewFile();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }



            //Toast.MakeText(Android.App.Application.Context, "Yes", ToastLength.Short).Show();

        }
        public bool WriteFile(string content, string sid, string eid, string quality, int clientType, string urls, List<long> length,List<string> playUrls,string aid)
        {

            //Toast.MakeText(Android.App.Application.Context, Android.OS.Environment.DataDirectory.Path, ToastLength.Long).Show();
            //Android.OS.Environment.DataDirectory

            try
            {
                var biliPath = "/storage/emulated/0/Android/data/tv.danmaku.bili/download/";
                if (clientType == 1)
                {
                    biliPath = "/storage/emulated/0/Android/data/com.bilibili.app.blue/download/";
                }
                Java.IO.File bilidirv = new Java.IO.File(biliPath);
                if (!bilidirv.Exists())
                {
                    return false;
                }

                string path = biliPath + "s_" + sid + "/";
                Java.IO.File dirv = new Java.IO.File(path);
                if (!dirv.Exists())
                {
                    dirv.Mkdirs();
                }

                string e_path = path + eid + "/";
                Java.IO.File e_path_dirv = new Java.IO.File(e_path);
                if (!e_path_dirv.Exists())
                {
                    e_path_dirv.Mkdirs();
                }
                string json_path = e_path + "/entry.json";
                Java.IO.File json_path_file = new Java.IO.File(json_path);
                if (!json_path_file.Exists())
                {
                    json_path_file.CreateNewFile();
                }

                FileStream fileOS = new FileStream(json_path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                Java.IO.BufferedWriter buf1 = new Java.IO.BufferedWriter(new Java.IO.OutputStreamWriter(fileOS));
                buf1.Write(content, 0, content.Length);
                buf1.Flush();
                buf1.Close();


                string video_path = e_path + "/" + quality + "/";
                Java.IO.File video_path_dirv = new Java.IO.File(video_path);
                if (!video_path_dirv.Exists())
                {
                    video_path_dirv.Mkdirs();
                }


                string indexjson_path = video_path + "index.json";
                Java.IO.File indexjson_path_file = new Java.IO.File(indexjson_path);
                if (!indexjson_path_file.Exists())
                {
                    indexjson_path_file.CreateNewFile();
                }
                using (FileStream indexfile = new FileStream(indexjson_path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                {
                    Java.IO.BufferedWriter buf2 = new Java.IO.BufferedWriter(new Java.IO.OutputStreamWriter(indexfile));
                    buf2.Write(urls, 0, urls.Length);
                    buf2.Flush();
                    buf2.Close();
                }

                for (int i = 0; i < length.Count; i++)
                {
                    string sum_path = video_path + i+".blv.4m.sum";
                    Java.IO.File sum_path_file = new Java.IO.File(sum_path);
                    if (!sum_path_file.Exists())
                    {
                        sum_path_file.CreateNewFile();
                    }
                    using (FileStream indexfile = new FileStream(sum_path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                    {
                        Java.IO.BufferedWriter buf2 = new Java.IO.BufferedWriter(new Java.IO.OutputStreamWriter(indexfile));
                        string c = "{\"length\":"+ length[i]+ "}";
                        buf2.Write(c, 0, c.Length);
                        buf2.Flush();
                        buf2.Close();
                    }

                    string blv_path = video_path + i + ".blv";
                    Java.IO.File blv_path_file = new Java.IO.File(blv_path);
                   
                        var did = OpenDownload(playUrls[i], blv_path_file, eid, eid + "-" + i.ToString());
                   
                   
                }



                

                string danmu_path = e_path + "/danmaku.xml";
                Java.IO.File danmu_path_file = new Java.IO.File(danmu_path);
                if (!danmu_path_file.Exists())
                {
                    danmu_path_file.CreateNewFile();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }



            //Toast.MakeText(Android.App.Application.Context, "Yes", ToastLength.Short).Show();

        }

        public long OpenDownload(string uri, Java.IO.File saveFile,string eid,string index)
        {

            //   DownloadManager downloadManager = (DownloadManager)Forms.Context.GetSystemService("DOWNLOAD_SERVICE");
            try
            {

                DownloadManager downloadManager = (DownloadManager)Forms.Context.GetSystemService(Context.DownloadService);
                //DownloadManager downloadManager = DownloadManager.FromContext(Forms.Context);
                DownloadManager.Request request = new DownloadManager.Request(Android.Net.Uri.Parse(uri));
             

             
                //request.SetTitle(index);
                request.AddRequestHeader("Referer", " https://www.bilibili.com/bangumi/play/ep" + eid);
                // request.AddRequestHeader("Referer", "https://www.bilibili.com/blackboard/html5player.html?crossDomain=true");
                

               
                request.AddRequestHeader("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");

                request.SetDestinationUri(Android.Net.Uri.FromFile(saveFile));

                //request.SetDestinationInExternalPublicDir("dirType", "/mydownload/" + cid + "-" + index + ".flv");
                //request.SetNotificationVisibility(DownloadVisibility.Hidden);
                request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);

                //DownloadManager manager = (DownloadManager)Context.getSystemService(Context.DownloadService);

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