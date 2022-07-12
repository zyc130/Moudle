using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
