using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 基础信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        protected sys_user _user;
        /// <summary>
        /// HttpContextAccessor
        /// </summary>
        protected readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        public BaseController
            (IHttpContextAccessor _httpContextAccessor
            , IUserBusiness _userBusiness)
        {
            httpContextAccessor = _httpContextAccessor;

            var claims = httpContextAccessor.HttpContext?.User.Claims;

            _user = _userBusiness.QueryByName(claims?.FirstOrDefault()?.Subject?.Name??string.Empty);
        }
    }
}
