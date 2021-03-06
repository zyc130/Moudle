using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{

    /// <summary>
    /// 角色
    /// </summary>
    public class RoleController : BaseController
    {
        IRoleBusiness roleBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_roleBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_context"></param>
        /// <param name="_user"></param>
        public RoleController(IRoleBusiness _roleBusiness
            , IMapper _mapper
            , IHttpContextAccessor _context, IUserBusiness _user
            , IDictionaryDetailBusiness dictionaryDetailBusiness) : base(_context, _user,_mapper, dictionaryDetailBusiness)
        {
            roleBusiness = _roleBusiness;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_role))]
        public IActionResult Add(SystemDto t)
        {
            var role = mapper.Map<SystemDto, sys_role>(t);
            role.create_id = user?.id ?? 0;
            role.update_id = user?.id ?? 0;
            return new ApiResult(roleBusiness.Add(role));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_role))]
        public IActionResult Query(long id)
        {
            return new ApiResult(roleBusiness.Find(id));
        }


        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_role))]
        public IActionResult QueryAll()
        {
            return new ApiResult(roleBusiness.FindAll());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return new ApiResult(roleBusiness.Delete(id));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(SystemDto t)
        {
            var role = mapper.Map<SystemDto, sys_role>(t);
            role.update_id = user?.id ?? 0;
            return new ApiResult(roleBusiness.Update(role));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_role))]
        public IActionResult QueryPage([FromQuery] SystemPageDto t)
        {
            var data = roleBusiness.QueryPage(t);
            data?.DataList.SetName(userBusiness.FindAll())
                .ToDictionaryName(dictionaryDetailBusiness);
            return new ApiResult(data);
        }
    }
}
