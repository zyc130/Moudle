using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Stock;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.DataAccess.Interface.Stock
{
    public interface IStockDataAccess: IBaseDataAccess
    {
        /// <summary>
        /// 获取可用货位数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetLocationEmptyCount(ELocationType type);

        /// <summary>
        /// 获取巷道优先列表
        /// </summary>
        /// <param name="type"></param>
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
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<StockShowDto> QueryPage(StockPageDto page);
    }
}
