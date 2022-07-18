using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.Stock;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 库存信息
    /// </summary>
    public class StockController : BaseController
    {

        IStockBusiness stockBusiness;
        IStockDetailBusiness stockDetailBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_stockBusiness"></param>
        /// <param name="_stockDetailBusiness"></param>
        public StockController(IHttpContextAccessor _httpContextAccessor, IUserBusiness _userBusiness, IMapper _mapper
            , IStockBusiness _stockBusiness
            , IStockDetailBusiness _stockDetailBusiness
            , IDictionaryDetailBusiness dictionaryDetailBusiness) : base(_httpContextAccessor, _userBusiness, _mapper,dictionaryDetailBusiness)
        {
            stockBusiness = _stockBusiness;
            stockDetailBusiness = _stockDetailBusiness;
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
            data?.DataList.SetName(userBusiness.FindAll()).ToDictionaryName(dictionaryDetailBusiness);
            return new ApiResult(data);
        }

        /// <summary>
        /// 库存详情
        /// </summary>
        /// <param name="stockId">库存id</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(stock_detail))]
        public IActionResult QueryByDetail(long stockId)
        {
            var _stock = stockBusiness.Find(stockId);
            var details = stockDetailBusiness.QueryByStockId(stockId);
            details?.ForEach(a =>
            {
                a.location_code = _stock.location_code;
            });
            return new ApiResult(details);
        }
    }
}
