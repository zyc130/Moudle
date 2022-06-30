using WMS.Moudle.Utility.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility.Serveice
{
    internal class RequestHelper : IRequestHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="payLoad"></param>
        /// <param name="contentType"></param>
        /// <param name="encode"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string SendRequest(string url, string payLoad = "", string contentType = "application/json", string encode = "utf-8", string method = "POST")
        {
            if (!url.ToLower().Contains("http://") && !url.ToLower().Contains("https://"))
                return string.Empty;
            HttpWebRequest request = null;
            try
            {
                //发送请求
                request = (HttpWebRequest)WebRequest.Create(url);
                //响应超时
                request.Timeout = 5000;
                request.ContentType = contentType;
                request.Method = method;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36";
                //清楚缓存
                request.Headers.Set("Pragma", "no-cache");
                if (!string.IsNullOrWhiteSpace(payLoad))
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(payLoad);
                    request.ContentLength = data.Length;
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                }
                //页面编码格式
                Encoding encoding = Encoding.GetEncoding(encode);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader streamreader = new StreamReader(stream, encoding))
                {
                    //获取数据
                    return streamreader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                request.Abort();
            }
        }
    }
}
