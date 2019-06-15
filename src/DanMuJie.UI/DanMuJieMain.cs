using BilibiliDM_PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanMuJie.UI
{
    public class DanMuJieMain : DMPlugin
    {
        private readonly MainWindow _window;

        public DanMuJieMain()
        {
            _window = new MainWindow();

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
            _window.Show();
            _window.Activate();
        }

        public override void Stop()
        {
            base.Stop();
            //請勿使用任何阻塞方法
            this.Log("弹幕街 Stoped!");
            this.AddDM("弹幕街 Stoped!", true);
        }

        public override void Start()
        {
            base.Start();
            //請勿使用任何阻塞方法
            this.Log("弹幕街 Started!");
            this.AddDM("弹幕街 Started!", true);
        }

        private void DanMuJieMain_ReceivedRoomCount(object sender, ReceivedRoomCountArgs e)
        {
            throw new NotImplementedException();
        }

        private void DanMuJieMain_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            string commentText = e.Danmaku.CommentText;

            _window.SearchSongByKeyword(commentText);

            this.AddDM("开始播放音乐");
        }

        private void DanMuJieMain_Disconnected(object sender, DisconnectEvtArgs e)
        {
            throw new NotImplementedException();
        }

        private void DanMuJieMain_Connected(object sender, ConnectedEvtArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
