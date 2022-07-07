using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.Base;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 货位
    /// </summary>
    public class LocationController : BaseController
    {
        ILocationBusiness locationBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_locationBusiness"></param>
        public LocationController(IHttpContextAccessor _httpContextAccessor, IUserBusiness _userBusiness, IMapper _mapper
            , ILocationBusiness _locationBusiness) : base(_httpContextAccessor, _userBusiness, _mapper)
        {
            locationBusiness = _locationBusiness;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Init(List<LocationInitDto> ts)
        {
            if (ts?.Count==0)
            {
                return new ApiResult((false,"入参不能为空!"));
            }
            return new ApiResult(locationBusiness.Init(ts, user?.id??0));
        }

        /// <summary>
        /// 获取货架行数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryRows()
        {
            return new ApiResult(locationBusiness.QueryRows());
        }

        /// <summary>
        /// 获取货位根据行号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryByRowNo(int rowNo)
        {
            return new ApiResult(locationBusiness.QueryByRowNo(rowNo));
        }
    }
}
