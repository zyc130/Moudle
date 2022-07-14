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

        /// <summary>
        /// 获取空货位
        /// </summary>
        /// <param name="roadwayNo"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public base_location GetEmptyLocation(int roadwayNo, ELocationType type)
        {
            return _client.Queryable<base_location, stock>((l, s) =>new object[]{
                JoinType.Left
                ,l.roadway_no == s.roadway_no
                && l.location_code == s.location_code
                && s.state == EState.Use.GetHashCode()
                && s.is_in_stock == EIsInStock.Yes.GetHashCode()
            }).Where((l, s) => l.location_type == type.GetHashCode() 
                    && string.IsNullOrWhiteSpace(s.location_code) 
                    && l.roadway_no==roadwayNo).OrderBy(l => l.sort_no).Take(1)
              .Select(l=>l).First();
        }

        /// <summary>
        /// 获取可用货位数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetLocationEmptyCount(ELocationType type)
        {
           return _client.Queryable<base_location, stock>((l, s) =>new object[]
            {
                JoinType.Left
                ,l.roadway_no == s.roadway_no 
                && l.location_code==s.location_code
                && s.state==EState.Use.GetHashCode()
                && s.is_in_stock == EIsInStock.Yes.GetHashCode()
            }).Where((l, s)=>l.location_type==type.GetHashCode() && string.IsNullOrWhiteSpace(s.location_code)).Count();
        }

        /// <summary>
        /// 获取巷道优先列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<int> GetRoadwayNo(ELocationType type)
        {
            var query = _client.Queryable<base_location, stock>((l, s) => new object[]
            {
                JoinType.Left
                ,l.roadway_no == s.roadway_no
                && l.location_code==s.location_code
                && s.state==EState.Use.GetHashCode()
                && s.is_in_stock == EIsInStock.Yes.GetHashCode()
            }).Where((l, s) => l.location_type == type.GetHashCode() && string.IsNullOrWhiteSpace(s.location_code))
            .PartitionBy(l => l.roadway_no).OrderBy(l => l.sort_no).Take(1);


            return query.Select(l => l.roadway_no).ToList();
        }
    }
}
