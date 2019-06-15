using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebServer
{
    public class LightHttpResponse
    {
        private string _requestFilePath;

        private string _requestFileFullPath;

        public byte[] ResponseBody { get; set; }

        public byte[] ResponseHeader { get; set; }

        public string Content_Type { get; set; }

        public string StatusCode { get; set; }

        public string StatusMsg { get; set; }

        public LightHttpResponse(string requestFilePath)
        {
            _requestFilePath = requestFilePath;

            // 注意：一定要按此顺序执行
            LoadRequestFileFullPath();
            LoadResponseBody();
            ConstructResponseHeader();
        }

        private void LoadRequestFileFullPath()
        {
            this._requestFileFullPath = Environment.CurrentDirectory + @"/Res/"+this._requestFilePath;
        }

        private void ConstructResponseHeader()
        {
            StringBuilder sb = new StringBuilder();
            SelectStateCodeAndStateMsg();
            sb.Append("HTTP/1.1 " + StatusCode + " " + StatusMsg + "\r\n");
            SelectContentType();
            sb.Append("Content-Type:" + Content_Type + ";charset=utf-8\r\n");
            // 响应头与响应体之间有一个空行，所以有两组"\r\n"
            sb.Append("Content-Length:" + ResponseBody.Length + "\r\n\r\n");
            this.ResponseHeader = Encoding.UTF8.GetBytes(sb.ToString());
        }

        private void LoadResponseBody()
        {
            // 判断请求的文件是否存在
            if (File.Exists(this._requestFileFullPath))
            {
                // 读取请求的文件
                using (FileStream fileStream = new FileStream(this._requestFileFullPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    this.ResponseBody = buffer;
                }
            }
            else
            {
                this.ResponseBody = new byte[0];
            }
        }

        private void SelectStateCodeAndStateMsg()
        {
            // 判断请求的文件是否存在
            if (File.Exists(this._requestFileFullPath))
            {
                this.StatusCode = "200";
                this.StatusMsg = "ok";
            }
            else
            {
                this.StatusCode = "404";
                this.StatusMsg = "Not found";
            }
        }

        private void SelectContentType()
        {
            string requestFileExt = Path.GetExtension(_requestFilePath);
            switch (requestFileExt)
            {
                case ".htm":
                case ".html":
                    this.Content_Type = "text/html";
                    break;
                default:
                    this.Content_Type = "text/plain";
                    break;
            }
        }
    }
}
