using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : BaseController
    {
        IJwtHelper jwtHelper;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_userBusiness"></param>
        /// <param name="_jwtHelper"></param>
        /// <param name="_mapper"></param>
        /// <param name="_contextAccessor"></param>
        public UserController(IUserBusiness _userBusiness
            , IJwtHelper _jwtHelper
            , IMapper _mapper
            , IHttpContextAccessor _contextAccessor) : base(_contextAccessor, _userBusiness, _mapper)
        {
            jwtHelper = _jwtHelper;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login)
        {
            //获取用户信息
            var user = userBusiness.QueryByName(login.name);
            if (user == null || !(user?.password.Equals(login.password) ??false))
            { 
                return new ApiResult((false,"用户名/密码错误！"));
            }
            //自定义信息
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.id.ToString()),
                new Claim(ClaimTypes.Name,user.name??String.Empty),
                new Claim("code",user?.code??string.Empty),
                new Claim("real_name",user?.real_name??string.Empty),
            };
            string token = jwtHelper.GetJwtToken(StaticParams.CUSTOMCONFIG.JwtOption, claims);

            return new ApiResult(data: token);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(UserDto t)
        {
            var user = mapper.Map<SystemDto, sys_user>(t);
            user.create_id = base.user?.id ?? 0;
            user.update_id = base.user?.id ?? 0;
            return new ApiResult(userBusiness.Add(user));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Query(long id)
        {
            var user =   httpContextAccessor.HttpContext?.User;
            return new ApiResult(userBusiness.Find(id));
        }


        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryAll()
        {
            return new ApiResult(userBusiness.FindAll());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return new ApiResult(userBusiness.Delete(id));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(UserDto t)
        {
            var user = mapper.Map<UserDto, sys_user>(t);
            user.update_id = base.user?.id ?? 0;
            return new ApiResult(userBusiness.Update(user));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryPage([FromQuery] SystemPageDto t)
        {
            return new ApiResult(userBusiness.QueryPage(t));
        }
    }
}
