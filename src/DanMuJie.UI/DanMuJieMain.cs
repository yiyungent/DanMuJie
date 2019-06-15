using BilibiliDM_PluginFramework;
using Meting4Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DanMuJie.UI
{
    public class DanMuJieMain : DMPlugin
    {
        private static MainWindow _window;

        public Meting Api { get; set; }

        public DanMuJieMain()
        {
            _window = new MainWindow(this);

            this.Connected += DanMuJieMain_Connected;
            this.Disconnected += DanMuJieMain_Disconnected;
            this.ReceivedDanmaku += DanMuJieMain_ReceivedDanmaku;
            this.ReceivedRoomCount += DanMuJieMain_ReceivedRoomCount;
            this.PluginAuth = "yiyun";
            this.PluginName = "弹幕街";
            this.PluginDesc = "使用弹幕...";
            this.PluginCont = "yiyungent@126.com";
            this.PluginVer = "v0.0.1";

        }

        public override void Admin()
        {
            if (_window == null)
            {
                _window = new MainWindow(this);
            }
            _window.Show();
            _window.Activate();
        }

        public override void Stop()
        {
            //請勿使用任何阻塞方法
            this.Log("弹幕街 Stoped!");
            this.AddDM("弹幕街 Stoped!", true);

            _window.Close();
            Application.Current.Shutdown();

            base.Stop();
        }

        public override void Start()
        {
            //請勿使用任何阻塞方法
            this.Log("弹幕街 Started!");
            this.AddDM("弹幕街 Started!", true);

            if (_window == null)
            {
                _window = new MainWindow(this);
            }

            Api = new Meting();
            Api.Format = true;

            base.Start();
        }

        private void DanMuJieMain_ReceivedRoomCount(object sender, ReceivedRoomCountArgs e)
        {
        }

        private void DanMuJieMain_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            string commentText = e.Danmaku.CommentText;
            this.AddDM(commentText);
            if (!commentText.Trim().StartsWith("dmj"))
            {
                return;
            }
            this.AddDM(commentText);
            string[] apiAndSongInfo = commentText.Trim().Split(new char[] { '-' });
            if (apiAndSongInfo != null && apiAndSongInfo.Length >= 3)
            {
                ServerProvider api = SelectServerProvider(apiAndSongInfo[1]);
                this.Api.Server = api;
                try
                {
                    var songs = Api.SearchObj(apiAndSongInfo[2]);

                    if (songs != null && songs.Length >= 1)
                    {
                        _window.ReLoadSearchResult(songs);

                        var song = songs.FirstOrDefault();

                        try
                        {
                            _window.ShowMessage("正在播放: " + song.name + " - " + string.Join(",", song.artist));
                        }
                        catch (Exception ex)
                        { }

                        this.AddDM("开始播放: " + song.name);

                        try
                        {
                            PlayMusicHelper.AddPlayList(GetPlayUrlById(song.url_id));
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        try
                        {
                            _window.ShowMessage("无搜索结果: " + apiAndSongInfo[2]);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                catch (Exception ex)
                {
                    this.AddDM("错误: " + ex.Message);
                }
            }
        }

        private void DanMuJieMain_Disconnected(object sender, DisconnectEvtArgs e)
        {

        }

        private void DanMuJieMain_Connected(object sender, ConnectedEvtArgs e)
        {

        }

        private string GetPlayUrlById(string id)
        {
            return Api.UrlObj(id).url;
        }

        private ServerProvider SelectServerProvider(string api)
        {
            ServerProvider rtn;
            switch (api.ToLower())
            {
                case "163":
                case "netease":
                    rtn = ServerProvider.Netease;
                    break;
                case "qq":
                case "tencent":
                    rtn = ServerProvider.Tencent;
                    break;
                case "baidu":
                    rtn = ServerProvider.Baidu;
                    break;
                case "kugou":
                    rtn = ServerProvider.Kugou;
                    break;
                case "xiami":
                    rtn = ServerProvider.Xiami;
                    break;
                default:
                    rtn = ServerProvider.Xiami;
                    break;
            }

            return rtn;
        }
    }
}
