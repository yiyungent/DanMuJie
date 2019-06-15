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
        public static DanMuJieMain DanMuJieMain { get; set; }

        //public CefSharp.Wpf.ChromiumWebBrowser Browser { get; set; }

        public MainWindow(DanMuJieMain danMuJieMain)
        {
            DanMuJieMain = danMuJieMain;

            InitializeComponent();

            this.TBUse.Text = @"使用说明: 发送以下
       dmj-qq-歌曲名
       音乐API有: qq, xiami, kugou, 163, baidu";
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

        public void ShowMessage(string message)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.LbMessage.Content = message;
                }));
            }
            catch (Exception ex)
            {

            }
        }

        public void ReLoadSearchResult(object data)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.SearchResult.DataContext = data;
                }));
            }
            catch (Exception ex)
            {

            }
        }

    }
}
