using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.DataAccess.Interface.Stock;
using WMS.Moudle.Entity.Enum;
using WMS.Moudle.Entity.Models;
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

        public base_location GetEmptyLocation(int roadwayNo, ELocationType type)
        {
            return stockDataAccess.GetEmptyLocation(roadwayNo, type);
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

        /// <summary>
        /// 获取巷道顺序
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<int> GetRoadwayNo(ELocationType type)
        {
            return stockDataAccess.GetRoadwayNo(type);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public stock Insert(stock t)
        {
           t.state = EState.Use.GetHashCode();
           return stockDataAccess.Insert(t);
        }
    }
}
