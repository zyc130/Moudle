using AutoMapper;
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
        protected sys_user? user;
        /// <summary>
        /// HttpContextAccessor
        /// </summary>
        protected readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// automapper
        /// </summary>
        protected IMapper mapper;
        private IUserBusiness userBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        public BaseController
            (IHttpContextAccessor _httpContextAccessor
            , IUserBusiness _userBusiness
            , IMapper _mapper)
        {
            httpContextAccessor = _httpContextAccessor;
            mapper= _mapper;
            userBusiness = _userBusiness;

            var claims = httpContextAccessor.HttpContext?.User.Claims;
            user = _userBusiness.QueryByName(claims?.FirstOrDefault()?.Subject?.Name??string.Empty);
        }

        /// <summary>
        /// 输出Excel文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName">文件名不带后缀(默认.xlsx)</param>
        /// <returns></returns>
        protected IActionResult WriteExcel(byte[] data,string fileName)
        {
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
        }
    }
}
