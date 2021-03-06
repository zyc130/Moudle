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
    /// 部门
    /// </summary>
    public class DeptController : BaseController
    {
        IDeptBusiness deptBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_deptBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_context"></param>
        /// <param name="_user"></param>
        public DeptController(IDeptBusiness _deptBusiness,IMapper _mapper
            , IHttpContextAccessor _context
            , IDictionaryDetailBusiness dictionaryDetailBusiness, IUserBusiness _user) : base(_context, _user, _mapper, dictionaryDetailBusiness)
        {
            deptBusiness = _deptBusiness;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dept))]
        public IActionResult Add(SystemDto t)
        {
            var dept=  mapper.Map<SystemDto, sys_dept>(t);
            dept.create_id = user?.id ?? 0;
            dept.update_id = user?.id ?? 0;
            return new ApiResult(deptBusiness.Add(dept));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dept))]
        public IActionResult Query(long id)
        {
            return new ApiResult(deptBusiness.Find(id));
        }


        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dept))]
        public IActionResult QueryAll()
        {
            return new ApiResult(deptBusiness.FindAll());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return new ApiResult(deptBusiness.Delete(id));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(SystemDto t)
        {
            var dept = mapper.Map<SystemDto, sys_dept>(t);
            dept.update_id = user?.id??0;
            return new ApiResult(deptBusiness.Update(dept));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dept))]
        public IActionResult QueryPage([FromQuery] SystemPageDto t)
        {
            var data = deptBusiness.QueryPage(t);
            data.DataList?.SetName(userBusiness.FindAll())
                .ToDictionaryName(dictionaryDetailBusiness);
            return new ApiResult(data);
        }
    }
}
