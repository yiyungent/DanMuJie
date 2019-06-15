using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServer
{
    public class LightHttpRequest
    {
        /// <summary>
        /// 请求报文
        /// </summary>
        private string _requestMsg;

        /// <summary>
        /// 请求文件地址（包括？后带的参数,不包括http://xx.xx.xx）
        /// </summary>
        public string FilePath { get; set; }

        public LightHttpRequest(string requestMsg)
        {
            this._requestMsg = requestMsg;

            ProcessRequestMsg();
        }

        private void ProcessRequestMsg()
        {
            // 每一行的报文信息
            string[] requestMsgs = this._requestMsg.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            // 请求首部（第一行）
            string requestHeader = requestMsgs[0];
            // 提取请求文件地址（包括？后带的参数）
            this.FilePath = requestHeader.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }
}
