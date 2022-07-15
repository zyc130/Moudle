using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Stock;
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
        /// 完成任务更新库存
        /// </summary>
        /// <param name="t"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        (bool, string) Finish(stock t, sys_user user);

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
    }
}
