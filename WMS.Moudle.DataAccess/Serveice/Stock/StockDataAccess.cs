using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.Stock;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Stock;
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
            return _client.Queryable<base_location, stock>((l, s) => new object[]
            {
                JoinType.Left
                ,l.roadway_no == s.roadway_no
                && l.location_code==s.location_code
                && s.state==EState.Use.GetHashCode()
                && s.is_in_stock == EIsInStock.Yes.GetHashCode()
            }).Where((l, s) => l.location_type == type.GetHashCode() && string.IsNullOrWhiteSpace(s.location_code))
            .PartitionBy(l => l.roadway_no).OrderBy(l => l.sort_no).Take(1).Select(l => l.roadway_no).ToList();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PageData<StockShowDto> QueryPage(StockPageDto page)
        {
            var query = _client.Queryable<stock, stock_detail>((s, d) =>new object[]
            {
                JoinType.Left,s.id==d.stock_id
            }).Where((s,d)=>s.is_in_stock== EIsInStock.Yes.GetHashCode() && s.state == EState.Use.GetHashCode())
            .Where((s,d)=>s.location_state==ELocationState.Use.GetHashCode() || s.location_state== ELocationState.OutStockStop.GetHashCode())
            .WhereIF(page.RoadWayNo!=null,s=>s.roadway_no==page.RoadWayNo.GetHashCode())
            .WhereIF(!string.IsNullOrWhiteSpace(page.LocationCode),s=>s.location_code.Contains(page.LocationCode??string.Empty))
            .WhereIF(!string.IsNullOrWhiteSpace(page.Code), (s,d) => d.fabrication_no.StartsWith(page.Code??string.Empty))
            .WhereIF(!string.IsNullOrWhiteSpace(page.Type), (s, d) => d.piece_code.StartsWith(page.Type??string.Empty))
            .WhereIF(!page.IsSpecialOut??false, (s, d) => s.location_state!=ELocationState.OutStockStop.GetHashCode())
            .Select<StockShowDto>("s.*,d.piece_code,d.fabrication_no");

            return new PageData<StockShowDto>()
            {
                DataList = query.ToPageList(page.pageIndex, page.pageSize),
                PageIndex = page.pageIndex,
                PageSize = page.pageSize,
                TotalCount = query.Count()
            };
        }
    }
}
