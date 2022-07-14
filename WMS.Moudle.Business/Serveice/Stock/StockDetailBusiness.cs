using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.DataAccess.Interface.Stock;

namespace WMS.Moudle.Business.Serveice.Stock
{
    internal class StockDetailBusiness : IStockDetailBusiness
    {
        IStockDetailDataAccess detailDataAccess;

        public StockDetailBusiness(IStockDetailDataAccess _detailDataAccess)
        {
            detailDataAccess= _detailDataAccess;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="stock_detial"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Insert<stock_detial>(List<stock_detial> ts)
        {
            return detailDataAccess.Insert(ts).Count>0;
        }
    }
}
