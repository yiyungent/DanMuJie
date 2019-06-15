using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebServer;
using Meting4Net.Core;
using Meting4Net.Core.Models.Standard;
using System.Threading;
using System.Diagnostics;

namespace DanMuJie.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DanMuJieMain DanMuJieMain { get; set; }

        public Meting Api { get; set; }

        //public CefSharp.Wpf.ChromiumWebBrowser Browser { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Api = new Meting(ServerProvider.Xiami);
            Api.Format = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region CefSharp
            //this.Browser = new CefSharp.Wpf.ChromiumWebBrowser();
            ////browser.Address = @"http://147zxcv.xiaopangkj.space/player.html";

            ////this.WebBrowser.WebBrowser.Do
            //// 注意，第二个路径不要加 "/"
            //// 错误：@"/Res/index.html"

            //IPAddress iPAddress = IPAddress.Any;
            //IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 5893);
            //LightWebServer webServer = new LightWebServer(iPEndPoint);
            //webServer.Start();
            //this.Browser.Address = @"http://127.0.0.1:5893/player.html";

            //this.Content = this.Browser; 
            #endregion
        }

        #region 点击搜索
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Debug.Write("测试");
            string songKeyword = this.TxtSongKeyword.Text;

            new Thread(() =>
            {
                SearchSongByKeyword(songKeyword);
            }).Start();
        }
        #endregion

        #region 搜索歌曲通过关键词
        public void SearchSongByKeyword(string keyword)
        {
            Music_search_item[] songs = new Music_search_item[0];
            try
            {
                songs = Api.SearchObj(keyword);
            }
            catch (Exception ex)
            { }

            this.Dispatcher.Invoke(new Action(() =>
            {
                this.SearchResult.DataContext = songs;
            }));

            if (songs.Length >= 1)
            {
                PlayMusicHelper.PlayUrl(GetPlayUrlById(songs[0].url_id));
            }
        }
        #endregion

        private void BtnSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnSearch_Click(sender, e);
        }

        private string GetPlayUrlById(string id)
        {
            return Api.UrlObj(id).url;
        }
    }
}
