using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.Stock
{
    /// <summary>
    /// 库存明细
    /// </summary>
    public interface IStockDetailBusiness
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        bool Insert(List<stock_detail> ts);

        /// <summary>
        /// 获取库存详情
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        List<stock_detail> QueryByStockId(long stockId);
    }
}
