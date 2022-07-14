using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WMS.Moudle.Utility
{
    public class ResResult : IActionResult
    {
        FormatResResult result;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="result"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public ResResult(bool isSuccess, string msg, object data)
        {
            result = new FormatResResult(isSuccess, msg, data);
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            JsonSerializerSettings jsonserializersettings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            context.HttpContext.Response.ContentType = "application/json";
            return context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result, jsonserializersettings), Encoding.UTF8);
        }
    }

    internal class FormatResResult
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="result"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public FormatResResult(bool result, string msg, object data)
        {
            ResType = result;
            ResMessage = msg;
            ResData = data;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool ResType { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string ResMessage { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object ResData { get; set; }
    }
}
