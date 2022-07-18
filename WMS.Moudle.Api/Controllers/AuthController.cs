using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Utility;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    public class AuthController : BaseController
    {
        IRoleMenuBusiness roleMenuBusiness;
        IRoleBusiness roleBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_roleBusiness"></param>
        /// <param name="_roleMenuBusiness"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        public AuthController(
            IRoleBusiness _roleBusiness
            , IRoleMenuBusiness _roleMenuBusiness
            , IHttpContextAccessor _httpContextAccessor
            , IUserBusiness _userBusiness
            , IMapper _mapper
            , IDictionaryDetailBusiness dictionaryDetailBusiness) : base(_httpContextAccessor, _userBusiness, _mapper, dictionaryDetailBusiness)
        {
            roleMenuBusiness = _roleMenuBusiness;
            roleBusiness = _roleBusiness;
        }

        /// <summary>
        /// 查询角色权限清单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryByRoleId(long roleId)
        {
            return new ApiResult(roleMenuBusiness.FindByRoleId(roleId));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(AuthDto t)
        {
            var role = roleBusiness.Find(t.roleId);
            if (role == null)
            {
                return new ApiResult((false, "角色id不存在!"));
            }
            return new ApiResult(roleMenuBusiness.Save(t));
        }
    }
}
