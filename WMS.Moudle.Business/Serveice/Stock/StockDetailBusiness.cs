using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.DataAccess.Interface.Stock;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Serveice.Stock
{
    internal class StockDetailBusiness : IStockDetailBusiness
    {
        IStockDetailDataAccess detailDataAccess;

        public StockDetailBusiness(IStockDetailDataAccess _detailDataAccess)
        {
            detailDataAccess = _detailDataAccess;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public bool Insert(List<stock_detail> ts)
        {
            return detailDataAccess.Insert(ts) > 0;
        }

        /// <summary>
        /// 获取库存详情
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public List<stock_detail> QueryByStockId(long stockId)
        {
            return detailDataAccess.Query<stock_detail>(a => a.stock_id == stockId).ToList();
        }
    }
}
