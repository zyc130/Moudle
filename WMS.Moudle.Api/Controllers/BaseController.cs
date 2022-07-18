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
        protected readonly sys_user user;
        /// <summary>
        /// HttpContextAccessor
        /// </summary>
        protected readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// automapper
        /// </summary>
        protected IMapper mapper;

        /// <summary>
        /// 用户
        /// </summary>
        protected IUserBusiness userBusiness;

        /// <summary>
        /// 字典
        /// </summary>
        protected IDictionaryDetailBusiness dictionaryDetailBusiness;

        /// <summary>
        /// 构造2
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="dictionaryDetailBusiness"></param>
        public BaseController
            (IHttpContextAccessor httpContextAccessor
            , IUserBusiness userBusiness
            , IMapper mapper
            , IDictionaryDetailBusiness dictionaryDetailBusiness)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mapper= mapper;
            this.userBusiness = userBusiness;
            this.dictionaryDetailBusiness = dictionaryDetailBusiness;

            var claims = httpContextAccessor.HttpContext?.User.Claims;
            user = userBusiness.QueryByName(claims?.FirstOrDefault()?.Subject?.Name ?? string.Empty) ?? new sys_user() { id = 0 };
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
