using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuController : BaseController
    {
        IMenuBusiness menuBusiness;
        IMapper mapper;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_menuBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="_context"></param>
        /// <param name="_user"></param>
        public MenuController(IMenuBusiness _menuBusiness
                        , IMapper mapper
                        , IHttpContextAccessor _context, IUserBusiness _user) : base(_context, _user)
        {
            menuBusiness = _menuBusiness;
            this.mapper = mapper;   
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(MenuDto t)
        {
            sys_menu menu= mapper.Map<MenuDto,sys_menu>(t);
            menu.create_id = _user?.id ?? 0;
            menu.update_id = _user?.id ?? 0;
            return new ApiResult( menuBusiness.Add(menu));
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public IActionResult Update(MenuDto t)
        {
            sys_menu menu = mapper.Map<MenuDto, sys_menu>(t);
            menu.update_id = _user?.id ?? 0;
            return new ApiResult(menuBusiness.Udpate(menu));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return new ApiResult(menuBusiness.Delete(id));
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryTree()
        {
            return new ApiResult(menuBusiness.QueryTree());
        }
    }
}
