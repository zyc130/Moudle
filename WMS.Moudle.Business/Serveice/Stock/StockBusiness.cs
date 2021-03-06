using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.DataAccess.Interface.Stock;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Stock;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Enum;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.CommonEnum;
using static WMS.Moudle.Entity.Enum.TaskEnum;

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

        /// <summary>
        /// 完成任务更新库存(调用方加事务)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public (bool isSuccess, string msg) Finish(stock t, sys_user user)
        {
            if (t == null)
            {
                return (false, "库存信息不存在!");
            }
            t.location_state = t.is_in_stock == EIsInStock.Yes.GetHashCode() ? ELocationState.Use.GetHashCode() : ELocationState.Empty.GetHashCode();
            t.update_id = user.id;
            if (!UpdateLocationState(t))
            {
                return (false, "更新库存失败!");
            }
            //出库任务，更新入库货位未在用
            if (t.is_in_stock == EIsInStock.No.GetHashCode())
            {
                if (!UpdateStateStop(t))
                {
                    return (false, "置空库存失败!");
                }
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PageData<StockShowDto> QueryPage(StockPageDto page)
        {
            return stockDataAccess.QueryPage(page);
        }

        /// <summary>
        /// 获取库存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public stock GetStockToOut(MoudleOutDto t)
        {
            return stockDataAccess.Query<stock>
                (a => a.id == t.id 
                && a.state==EState.Use.GetHashCode() 
                && a.is_in_stock==EIsInStock.Yes.GetHashCode()
                && (a.location_state==ELocationState.Use.GetHashCode() || a.location_state == ELocationState.OutStockStop.GetHashCode()))
                   .WhereIF(!(t.is_special_out ?? false), a => a.location_state != ELocationState.OutStockStop.GetHashCode())
                   .WhereIF(!(t.is_special_out ?? false), a => a.task_type != ETaskType.PalletEmptyIn.GetHashCode())
                   .WhereIF(t.is_part??false,a=>a.task_type==ETaskType.MoudleIn.GetHashCode())
                   .First();
        }

        /// <summary>
        /// 更新货位状态
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool UpdateLocationState(stock t)
        {
            return stockDataAccess.UpdateColumns(t, u => new { u.update_id, u.update_time, u.location_state }, w => w.id == t.id && w.state==EState.Use.GetHashCode());
        }

        /// <summary>
        /// 获取出库中库存信息
        /// </summary>
        /// <param name="roadwayNo"></param>
        /// <param name="location_code"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public stock GetOutStock(int roadwayNo, string location_code)
        {
            return stockDataAccess.Query<stock>(a => a.roadway_no == roadwayNo
            && a.location_code == location_code
            && a.state == EState.Use.GetHashCode()
            && a.location_state == ELocationState.OutStock.GetHashCode()).First();
        }

        /// <summary>
        /// 更新货位出库中
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateOutStock(stock t)
        {
            return stockDataAccess.UpdateColumns(t
                , u => new { u.update_id, u.update_time, u.location_state, u.is_part }
                , w => w.id == t.id && w.state == EState.Use.GetHashCode());
        }

        public stock Find(long id)
        {
            return stockDataAccess.Find<stock>(id);
        }


        #region private


        /// <summary>
        /// 货位置空
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool UpdateStateStop(stock t)
        {
            t.state = EState.Stop.GetHashCode();
            return stockDataAccess.UpdateColumns(t, u => new { u.update_id, u.update_time, u.state }, w => w.location_code == t.location_code
            && w.roadway_no == t.roadway_no
            && w.state == EState.Use.GetHashCode()
            && w.is_in_stock == EIsInStock.Yes.GetHashCode());
        }

        /// <summary>
        /// 根据任务号获取库存信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public stock QueryByTaskId(long taskId)
        {
            return stockDataAccess.Query<stock>(a => a.task_id == taskId).First();
        }

        #endregion

    }
}
