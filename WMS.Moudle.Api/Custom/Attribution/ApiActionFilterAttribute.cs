using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Api.Custom.Attribution
{
    /// <summary>
    /// 请求过滤器
    /// </summary>
    public class ApiActionFilterAttribute : ActionFilterAttribute
    {
        ILogge<ApiActionFilterAttribute> logge;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_logge"></param>
        public ApiActionFilterAttribute(ILogge<ApiActionFilterAttribute> _logge)
        {
            logge= _logge;
        }

        /// <summary>
        /// 请求记录
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            IEnumerable<Claim>? claims = null;
            if (actionContext.HttpContext?.User != null)
            {
                claims = actionContext.HttpContext.User.Claims;
            }
            //记录信息
            logge.Info($"{actionContext?.HttpContext?.Request.Path.Value},{JsonConvert.SerializeObject(actionContext?.ActionArguments)},{claims?.FirstOrDefault()?.Subject?.Name??string.Empty}");
        }
    }
}
