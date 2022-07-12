using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Task;
using WMS.Moudle.DataAccess.Interface.Task;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Enum;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility.Interface;
using static WMS.Moudle.Entity.Enum.TaskEnum;
using WMS.Moudle.Utility.Extend;
using WMS.Moudle.Business.Interface.System;
using static WMS.Moudle.Entity.Enum.CommonEnum;
using WMS.Moudle.Redis.Interface;
using WMS.Moudle.Utility;
using WMS.Moudle.Business.Interface.Stock;

namespace WMS.Moudle.Business.Serveice.Task
{
    /// <summary>
    /// 任务
    /// </summary>
    internal class TaskBusiness : ITaskBusiness
    {
        ITaskDataAccess taskDataAccess;
        IMapper mapper;
        IExcuteHelper excuteHelper;
        ITaskDetailBusiness taskDetailBusiness;
        IConfigBusiness configBusiness;
        ITaskRedis taskRedis;
        IStockBusiness stockBusiness;
        public TaskBusiness(ITaskDataAccess _taskDataAccess
            , IMapper _mapper
            , IExcuteHelper _excuteHelper
            , ITaskDetailBusiness _taskDetailBusiness
            , IConfigBusiness _configBusiness
            , ITaskRedis _taskRedis
            , IStockBusiness _stockBusiness)
        {
            taskDataAccess = _taskDataAccess;
            mapper = _mapper;
            excuteHelper = _excuteHelper;
            taskDetailBusiness = _taskDetailBusiness;
            configBusiness = _configBusiness;
            taskRedis = _taskRedis;
            stockBusiness = _stockBusiness;
        }

        /// <summary>
        /// 模具入库
        /// </summary>
        /// <param name="t"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool, string) CreateMoudleIn(MoudleInDto t, sys_user user)
        {
            var _task = mapper.Map<task>(t);
            _task.is_in_stock = EIsInStock.Yes.GetHashCode();
            //模具类型
            if (_task.task_type== ETaskType.MoudleIn.GetHashCode())
            {
                if ((_task.material_type??0)==0)
                {
                    return (false, $"任务类型为:{ETaskType.MoudleIn.ToDescription()},模具类型不能为空");
                }
            }
            //扫码验证
            if (IsScan() && string.IsNullOrWhiteSpace(t.pallet_barcode))
            {
                return (false, $"托盘码不能为空");
            }
            var codes = t.codes?.FindAll(a => !string.IsNullOrWhiteSpace(a))?.Distinct().ToList();
            if (_task.task_type == ETaskType.MoudleIn.GetHashCode()
                || _task.task_type == ETaskType.MoudleSetIn.GetHashCode())
            {
                if ((codes?.Count??0)==0)
                {
                    return (false, $"部件码不能为空");
                }
            }
            //货位验证
            int emptyCount = GetLocationEmptyCount(_task.task_type.ToEnum<ETaskType>()) - 1;
            if (emptyCount < 0)
            {
                return (false, "暂无空货位!");
            }
            var execFormat = taskDetailBusiness.FormatDetails(codes, user);
            if (!execFormat.Item1)
            {
                return (false, execFormat.Item2);
            }
            //要加物料入库唯一验证
            List<task_detail> details = execFormat.Item3;
            _task.create_id = user.id;
            _task.update_id = user.id;
            return Insert(_task, details);
        }

        /// <summary>
        /// 托盘入库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool, string) CreatePalletIn(task t)
        {
            int emptyCount = GetLocationEmptyCount(t.task_type.ToEnum<ETaskType>())-1;
            if (emptyCount < 0)
            {
                return (false, "暂无空货位!");
            }
            return Insert(t, null);
        }

        public bool CreatePalletOut(List<task> ts)
        {
            //判断托盘库存
            return taskDataAccess.Insert(ts)>0;
        }

        public task Insert(task t)
        {
            return taskDataAccess.Insert(t);
        }

        /// <summary>
        /// 获取模具物件行数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool, string, int) QueryMoudleCount(MoudleCountDto t)
        {
            int count = 0;
            switch (t.task_type)
            {
                case ETaskType.MoudleIn:
                    count = 1;
                    break;
                case ETaskType.MoudleSetIn:
                    if (t.material_type==null)
                    {
                        return (false, "物件类型不能为空!", 0);
                    }
                    count = QueryCount((EMaterialType)t.material_type);
                    break;
                case ETaskType.OtherIn:
                    count = 0;
                    break;
                default:
                    return (false, "参数错误", 0);
            }
            return (true, string.Empty, count);
        }

        /// <summary>
        /// 每天任务号索引
        /// </summary>
        /// <returns></returns>
        public string GetTaskNo()
        {
            long index = taskRedis.GetTaskIndex(null);
            //验证当天是否第一个
            if (index == 1)
            {
                var task = taskDataAccess.Query<task>(a => a.create_time >= DateTime.Now.Date).OrderByDescending(o => o.task_no)?.First();
                if (task != null)
                {
                    long start = ConvertHelper.ToLong(task.task_no.Remove(0, 8));

                    index = taskRedis.GetTaskIndex(start);
                    //自增值生成异常
                    if (index <= 1)
                    {
                        return String.Empty;
                    }
                }
            }
            return $"{DateTime.Now:yyyyMMdd}{index.ToString().PadLeft(4, '0')}";
        }

        /// <summary>
        /// 获取全部等待中的任务
        /// </summary>
        /// <returns></returns>
        public List<task> GetWaitAll()
        {
            return taskDataAccess.Query<task>(a => a.task_state == ETaskState.Wait.GetHashCode())?.ToList();
        }

        #region private

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="t"></param>
        /// <param name="detials"></param>
        /// <returns></returns>
        private (bool, string) Insert(task t, List<task_detail> details)
        {
            t.state = EState.Use.GetHashCode();
            t.task_state = ETaskState.Wait.GetHashCode();
            t.number = details?.Count ?? 0;

            string taskNo = GetTaskNo();
            if (string.IsNullOrWhiteSpace(taskNo))
            {
                return (false, "任务号生成异常!");
            }

            t.task_no = taskNo;
            return excuteHelper.Tran(() =>
            {
                var task = taskDataAccess.Insert(t);
                if (task?.id == 0)
                {
                    return (false, "任务创建失败!");
                }
                if (details?.Count > 0)
                {
                    details.ForEach(a =>
                    {
                        a.task_id = task.id;
                    });
                    if (taskDetailBusiness.Insert(details) == 0)
                    {
                        return (false, "任务创建明细失败!");
                    }
                }
                return (true, "任务创建成功!");
            });
        }

        /// <summary>
        /// 获取配置行数
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        private int QueryCount(EMaterialType materialType)
        {
            EConfigCode eConfigCode = new EConfigCode();
            switch (materialType)
            {
                case EMaterialType.Part:
                    eConfigCode = EConfigCode.moudle_part;
                    break;
                case EMaterialType.Whole:
                    eConfigCode = EConfigCode.moudle_whole;
                    break;
                case EMaterialType.Spare:
                    eConfigCode = EConfigCode.moudle_spare;
                    break;
            }
            return configBusiness.QueryValue(eConfigCode) ?? 0;
        }

        /// <summary>
        /// 是否需要扫码
        /// </summary>
        /// <returns></returns>
        private bool IsScan()
        {
            return (configBusiness.QueryValue(EConfigCode.is_scan_pallet) ?? 0) == 1 ? true : false;
        }

        /// <summary>
        /// 可用空货位
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public int GetLocationEmptyCount(ETaskType taskType)
        {
            //获取空货位数
            int emptyCount = stockBusiness.GetLocationEmptyCount(taskType == ETaskType.MoudleSetIn ? ELocationType.Big : ELocationType.Common);
            //获取未分配任务数
            var tasks = GetWaitAll();
            Predicate<task> match = (t) => taskType == ETaskType.MoudleSetIn ? t.task_type == ETaskType.MoudleSetIn.GetHashCode() : t.task_type != ETaskType.MoudleSetIn.GetHashCode();
            int waitCount = tasks?.FindAll(match)?.Count()??0;
            return emptyCount - waitCount;
        }


        #endregion

    }
}
