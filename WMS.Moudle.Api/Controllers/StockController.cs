using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.Stock;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 库存信息
    /// </summary>
    public class StockController : BaseController
    {

        IStockBusiness stockBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        public StockController(IHttpContextAccessor _httpContextAccessor, IUserBusiness _userBusiness, IMapper _mapper
            , IStockBusiness _stockBusiness) : base(_httpContextAccessor, _userBusiness, _mapper)
        {
            stockBusiness = _stockBusiness;
        }

        /// <summary>
        /// 获取库存列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet] 
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockShowDto))]
        public IActionResult QueryPage([FromQuery] StockPageDto t)
        {
            var data = stockBusiness.QueryPage(t);
            data?.DataList.SetName(userBusiness.FindAll());
            return new ApiResult(data);
        }
    }
}
