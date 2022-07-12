using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.DataAccess.Interface.Stock;
using WMS.Moudle.Entity.Enum;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.Business.Serveice.Stock
{
    internal class StockBusiness : IStockBusiness
    {
        IStockDataAccess stockDataAccess;
        public StockBusiness(IStockDataAccess stockDataAccess)
        {
            this.stockDataAccess = stockDataAccess;
        }

        /// <summary>
        /// 空货架数量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetLocationEmptyCount(ELocationType type)
        {
            return stockDataAccess.GetLocationEmptyCount(type);
        }

        public List<int> GetRoadwayNo(ELocationType type)
        {
            throw new NotImplementedException();
        }
    }
}
