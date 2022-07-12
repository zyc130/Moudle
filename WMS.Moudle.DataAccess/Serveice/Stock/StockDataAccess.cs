using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.Stock;
using WMS.Moudle.Entity.Enum;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.CommonEnum;
using static WMS.Moudle.Entity.Enum.TaskEnum;

namespace WMS.Moudle.DataAccess.Serveice.Stock
{
    internal class StockDataAccess : BaseDataAccess, IStockDataAccess
    {
        public StockDataAccess(ISqlSugarClient client) : base(client)
        {
        }

        public int GetLocationEmptyCount(ELocationType type)
        {
           return _client.Queryable<base_location, stock>((l, s) =>new object[]
            {
                JoinType.Left
                ,l.roadway_no == s.roadway_no 
                && l.location_code==s.location_code
                && s.state==EState.Use.GetHashCode()
                && s.is_in_stock == EIsInStock.Yes.GetHashCode()
            }).Where((l, s)=>l.location_type==type.GetHashCode()).Count();
        }
    }
}
