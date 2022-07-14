using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Business.Interface.Stock
{
    /// <summary>
    /// 库存明细
    /// </summary>
    public interface IStockDetailBusiness
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="stock_detial"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        bool Insert<stock_detial>(List<stock_detial> ts);
    }
}
