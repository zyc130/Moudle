using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility
{
    public class ApiResult : IActionResult
    {
        FormatResult result = new FormatResult();
        public ApiResult()
        {
            
        }
        public ApiResult(bool isSuccess = true)
        {
            result.Set(_isSuccess:isSuccess, _msg: !isSuccess ? "网络不给力！" : String.Empty);
        }

        public ApiResult((bool isSuccess, string msg) exec)
        {
            if (exec.isSuccess)
            {
                result.Set(_isSuccess: exec.isSuccess);
            }
            else
            {
                result.Set(_isSuccess: exec.isSuccess, _msg: string.IsNullOrWhiteSpace(exec.msg) ? "网络不给力!" : exec.msg);
            }
        }

        public ApiResult((bool isSuccess, string msg,object obj) exec)
        {
            if (exec.isSuccess)
            {
                result.Set(exec.obj, exec.isSuccess);
            }
            else
            {
                result.Set(_isSuccess: exec.isSuccess, _msg: string.IsNullOrWhiteSpace(exec.msg) ? "网络不给力!" : exec.msg);
            }
        }

        public ApiResult(object data=null, bool isSuccess = true, int code = 0, string msg = "")
        {
            result.Set(data, isSuccess,code,msg);
        }

        ///// <summary>
        ///// 入参异常
        ///// </summary>
        ///// <param name="models"></param>
        //public ApiResult(ModelStateDictionary models)
        //{
        //    result.isSuccess = false;
        //    result.msg = "参数错误";
        //    var modelErr = models.Values.FirstOrDefault(a => a.Errors.Count > 0);
        //    if (modelErr != null)
        //    {
        //        var err = modelErr.Errors.FirstOrDefault(a => !string.IsNullOrWhiteSpace(a.ErrorMessage));
        //        if (err != null)
        //        {
        //            result.msg = err.ErrorMessage;
        //        }
        //    }
        //}

        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool isSuccess
        {
            get
            {
                return result.isSuccess;
            }
            set
            {
                result.isSuccess = value;
            }
        }

        /// <summary>
        /// 默认0成功，自定义业务code
        /// </summary>
        public int code
        {
            get { return result.code; }
            set { result.code = value; }
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public object data 
        { 
            get { return result.data; }
            set { result.data = value; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg
        {
            get { return result.msg; }
            set { result.msg = value; }
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long stamp
        {
            get { return result.stamp; }
            set { result.stamp = value; }
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

    /// <summary>
    /// 接口返回信息
    /// </summary>
    internal class FormatResult
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool isSuccess = true;

        /// <summary>
        /// 默认0成功，自定义业务code
        /// </summary>
        public int code = 0;
        /// <summary>
        /// 返回信息
        /// </summary>
        public object data = string.Empty;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg = string.Empty;
        /// <summary>
        /// 时间戳
        /// </summary>
        public long stamp = 0;

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="_code"></param>
        /// <param name="_msg"></param>
        public void Set(object _data=null,bool _isSuccess=true,int _code = 0, string _msg = "")
        {
            isSuccess = _isSuccess;
            code = _code;
            msg = _msg;
            data = _data;
        }
    }
}
