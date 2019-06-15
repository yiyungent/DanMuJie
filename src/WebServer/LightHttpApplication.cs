using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    public class LightHttpApplication
    {
        private Socket _newSocket = null;

        private DelShowMsg _delShowMsg = null;

        public LightHttpApplication(Socket newSocket, DelShowMsg delShowMsg)
        {
            this._newSocket = newSocket;
            this._delShowMsg = delShowMsg;

            byte[] buffer = new byte[1024 * 1024 * 2];
            // 真实接收到的实际内容大小(因为不足buffer的大小时，会用0填充buffer)
            int receiveContentSize = _newSocket.Receive(buffer);
            if (receiveContentSize <= 0)
            {
                return;
            }
            // 只解码实际内容大小
            string msg = Encoding.UTF8.GetString(buffer, 0, receiveContentSize);
            // 展示接收到的请求报文
            _delShowMsg(msg);
            // 请求
            LightHttpRequest request = new LightHttpRequest(msg);
            // 处理请求
            ProcessRequest(request);
        }

        public void ProcessRequest(LightHttpRequest request)
        {

            // 构建响应(传入请求的文件内容，请求的文件地址)
            LightHttpResponse response = new LightHttpResponse(request.FilePath);
            // 响应
            this._newSocket.Send(response.ResponseHeader);
            this._newSocket.Send(response.ResponseBody);
        }
    }
}
