using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility.Interface
{
    /// <summary>
    /// 请求帮助类
    /// </summary>
    public interface IRequestHelper
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">api url</param>
        /// <param name="payLoad">post参数 json</param>
        /// <param name="contentType">格式</param>
        /// <param name="encode">编码</param>
        /// <param name="method">请求方式</param>
        /// <returns></returns>
        string SendRequest(string url, string payLoad = "", string contentType = "application/json", string encode = "utf-8", string method = "POST");
    }
}
