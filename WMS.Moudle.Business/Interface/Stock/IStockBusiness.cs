using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Stock;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.Business.Interface.Stock
{
    /// <summary>
    /// 库存
    /// </summary>
    public interface IStockBusiness
    {
        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        stock Find(long id);

        /// <summary>
        /// 根据类型获取可用货架
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetLocationEmptyCount(ELocationType type);

        /// <summary>
        /// 根据类型获取巷道
        /// </summary>
        /// <returns></returns>
        List<int> GetRoadwayNo(ELocationType type);

        /// <summary>
        /// 根据巷道和货位类型获取货位
        /// </summary>
        /// <param name="roadwayNo"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        base_location GetEmptyLocation(int roadwayNo, ELocationType type);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        stock Insert(stock t);

        /// <summary>
        /// 完成任务更新库存(多方操作调用方加事务)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        (bool isSuccess, string msg) Finish(stock t, sys_user user);

        /// <summary>
        /// 根据任务号获取库存信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        stock QueryByTaskId(long taskId); 

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<StockShowDto> QueryPage(StockPageDto page);

        /// <summary>
        /// 获取出库库存信息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        stock GetStockToOut(MoudleOutDto t);

        /// <summary>
        /// 获取出库中库存信息
        /// </summary>
        /// <param name="roadwayNo"></param>
        /// <param name="location_code"></param>
        /// <returns></returns>
        stock GetOutStock(int roadwayNo, string location_code);

        /// <summary>
        /// 更新货位状态
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool UpdateLocationState(stock t);

        /// <summary>
        /// 更新货位出库中
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool UpdateOutStock(stock t);
    }
}
