using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer
{
    public delegate void DelShowMsg(string msg);

    public class LightWebServer
    {
        private Socket _listenSocket;

        public LightWebServer(IPEndPoint iPEndPoint)
        {
            this._listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._listenSocket.Bind(iPEndPoint);
        }

        public void Start()
        {
            this._listenSocket.Listen(10);
            ThreadPool.QueueUserWorkItem(new WaitCallback(BeginAccept));
        }

        #region 监听，负责给每一个连接创建新的Socket
        private void BeginAccept(object state)
        {
            // 不停的监听，每一个连接都要分配一个Socket与其通信
            while (true)
            {
                Socket newSocket = _listenSocket.Accept();
                // 开始处理连接上的请求
                LightHttpApplication myHttpApplication = new LightHttpApplication(newSocket, ShowMsg);
            }
        }
        #endregion

        public void ShowMsg(string msg)
        {

        }
    }
}
