using AutoMapper;
using WMS.Moudle.Business.Interface.Task;
using WMS.Moudle.DataAccess.Interface.Task;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility.Interface;
using static WMS.Moudle.Entity.Enum.TaskEnum;
using WMS.Moudle.Utility.Extend;
using WMS.Moudle.Business.Interface.System;
using static WMS.Moudle.Entity.Enum.CommonEnum;
using WMS.Moudle.Redis.Interface;
using WMS.Moudle.Utility;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.Entity.Dto.Wcs;
using Newtonsoft.Json.Linq;
using WMS.Moudle.Entity.Dto.Stock;
using Newtonsoft.Json;
using WMS.Moudle.Entity;
using SqlSugar;

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
        IStockDetailBusiness stockDetailBusiness;
        public TaskBusiness(ITaskDataAccess _taskDataAccess
            , IMapper _mapper
            , IExcuteHelper _excuteHelper
            , ITaskDetailBusiness _taskDetailBusiness
            , IConfigBusiness _configBusiness
            , ITaskRedis _taskRedis
            , IStockBusiness _stockBusiness
            , IStockDetailBusiness _stockDetailBusiness)
        {
            taskDataAccess = _taskDataAccess;
            mapper = _mapper;
            excuteHelper = _excuteHelper;
            taskDetailBusiness = _taskDetailBusiness;
            configBusiness = _configBusiness;
            taskRedis = _taskRedis;
            stockBusiness = _stockBusiness;
            stockDetailBusiness = _stockDetailBusiness;
        }

        public task Find(long id)
        {
            return taskDataAccess.Find<task>(id);
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
            if (_task.task_type == ETaskType.MoudleIn.GetHashCode())
            {
                if ((_task.material_type ?? 0) == 0)
                {
                    return (false, $"任务类型为:{ETaskType.MoudleIn.ToDescription()},模具类型不能为空");
                }
            }
            else
            {
                _task.material_type = 0;
            }
            //扫码验证
            if (IsScan() && string.IsNullOrWhiteSpace(t.pallet_barcode))
            {
                return (false, $"托盘码不能为空");
            }
            var codes = t.codes?.FindAll(a => !string.IsNullOrWhiteSpace(a))?.ToList();
            if (_task.task_type == ETaskType.MoudleIn.GetHashCode()
                || _task.task_type == ETaskType.MoudleSetIn.GetHashCode())
            {
                if ((codes?.Count??0)==0)
                {
                    return (false, $"部件码不能为空");
                }
                int count = QueryCount((_task.material_type ?? 0).ToEnum<EMaterialType>());
                if ((codes?.Count ?? 0) > count)
                {
                    return (false, $"部件码数量不能大于系统配置:{count}条");
                }
            }
            if (codes!=null && codes.Count!=codes.Distinct().Count())
            {
                return (false, $"部件码不能重复");
            }
            //货位验证
            int emptyCount = GetLocationEmptyCount(_task.task_type.ToEnum<ETaskType>()) - 1;
            if (emptyCount < 0)
            {
                return (false, "暂无空货位");
            }
            var execFormat = taskDetailBusiness.FormatDetails(codes, user);
            if (!execFormat.Item1)
            {
                return (false, execFormat.Item2);
            }
            //要加物料入库唯一验证（重要）
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
                return (false, "暂无空货位");
            }
            return Insert(t, null);
        }

        /// <summary>
        /// 托盘出库
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public bool CreatePalletOut(List<task> ts)
        {
            //判断托盘库存
            return taskDataAccess.Insert(ts)>0;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
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
            int waitCount = tasks?.FindAll(match)?.Count() ?? 0;
            return emptyCount - waitCount;
        }

        /// <summary>
        /// 获取巷道列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool, string, List<int>) GetRoadwayNo(TunnelDto t)
        {
            var _task = taskDataAccess.Query<task>(a => a.task_no == t.WMSTaskNum && a.task_state==ETaskState.Wait.GetHashCode())?.First();
            if (_task == null)
            {
                return (false, "任务号不存在!", null);
            }
            var items = stockBusiness.GetRoadwayNo(GetLocationType(_task.task_type.ToEnum<ETaskType>()));
            List<int> data = new();
            items?.ForEach(a =>
            {
                if (t?.SrmCurTunnelList.Any(b=>b.Values.Contains(a))??false)
                {
                    data.Add(a);
                }
            });
            return (true, string.Empty, data);
        }

        /// <summary>
        /// 获取分配货位
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool isSuccess, string msg, base_location locat, task _task) GetWareCell(WareCellDto t,sys_user user)
        {
            //获取任务
            var _task = taskDataAccess.Query<task>(a => a.task_no == t.WMSTaskNum && a.task_state == ETaskState.Wait.GetHashCode())?.First();
            if (_task == null)
            {
                return (false, "任务号不存在/已分配!", null, null);
            }
            //根据巷道和任务类型获取货位
            base_location location = stockBusiness.GetEmptyLocation(ConvertHelper.ToInt(t.TunnelNum),GetLocationType(_task.task_type.ToEnum<ETaskType>()));
            if (location == null)
            {
                return (false, "分配货位失败!", null, null);
            }

            _task.update_id = user.id;
            _task.roadway_no =ConvertHelper.ToInt(t.TunnelNum);
            _task.start_point = t.PickUpEquipmentNo;
            _task.now_point = t.PickUpEquipmentNo;
            _task.end_point = location.location_code;
            _task.task_state = ETaskState.Excuting.GetHashCode();
            //任务明细
            var details = taskDetailBusiness.GetByTaskId(_task.id);

            var exec = excuteHelper.Tran<string>(() =>
            {
                //写入任务货位信息
                if (!taskDataAccess.UpdateIgnore(_task, a => new { a.create_id, a.create_time, a.state }))
                {
                    return (false, "任务更新失败!");
                }
                //写入存库信息
                var exec = InsertStock(_task, details,user);
                if (!exec.Item1)
                {
                    return (false, "存库写入失败!");
                }
                return exec;
            });
            //返回货位信息
            if (!exec.Item1)
            {
                return (false,string.IsNullOrWhiteSpace(exec.Item2)?"网络不给力!": exec.Item2, null, null);
            }
            return (true,"", location, _task);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (bool,string) Cancel(long id,sys_user user)
        {
            var _task = Find(id);
            if (_task==null)
            {
                return (false,$"任务不存在!");
            }
            if (_task.task_state.ToEnum<ETaskState>()!=ETaskState.Wait)
            {
                return (false, $"任务进行中/已结束无法取消!");
            }
            _task.update_id = user.id;
            _task.task_state = ETaskState.Cancel.GetHashCode();
            if (!UpdateState(_task))
            {
                return (false, "操作失败!");
            }
            return (true,string.Empty);
        }

        /// <summary>
        /// 手动完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool,string) Finish(long id,sys_user user)
        {
            var _task = Find(id);
            if (_task == null)
            {
                return (false, $"任务不存在");
            }
            if (_task.task_state.ToEnum<ETaskState>() == ETaskState.Wait
                || _task.task_state>=ETaskState.Finish.GetHashCode())
            {
                return (false, $"任务排队中/已结束无法手动完成");
            }
            //校验wcs是否允许执行完成

            _task.update_id = user.id;
            _task.task_state = ETaskState.HandFinish.GetHashCode();
            //获取库存信息
            stock _stock;
            if (_task.is_in_stock==EIsInStock.Yes.GetHashCode())
            {
                _stock = stockBusiness.QueryByTaskId(_task.id);
            }
            else
            {
                _stock = stockBusiness.GetOutStock(_task.roadway_no??0,_task.start_point);
                _stock.is_in_stock = EIsInStock.No.GetHashCode();
            }

            var exec = excuteHelper.Tran<string>(() =>
            {
                if (!UpdateState(_task))
                {
                    return (false, "操作失败");
                }
                var (isSuccess, msg) = stockBusiness.Finish(_stock, user);
                if (!isSuccess)
                {
                    return (isSuccess, msg);
                }
                if (_task.is_in_stock == EIsInStock.No.GetHashCode())
                {
                    //写入存库信息
                    var details = taskDetailBusiness.GetByTaskId(_task.id);
                    return InsertStock(_task, details, user);
                }
                return (true, "");
            });

            return (exec.Item1, exec.Item2);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PageData<task> QueryPage(TaskPageDto page)
        {
            Expressionable<task> express = new();
            express.AndIF(page.is_in_stock != null, a => a.is_in_stock==page.is_in_stock.GetHashCode())
                .AndIF(page.task_type!=null, a => a.task_type==page.task_type.GetHashCode())
                .AndIF(!string.IsNullOrWhiteSpace(page.code),a=>a.end_point.Contains(page.code))
                .AndIF(!string.IsNullOrWhiteSpace(page.task_no), a => a.task_no.Contains(page.task_no));

            return taskDataAccess.QueryPage<task>(page.pageIndex, page.pageSize, express.ToExpression(), o => o.update_time, false);
        }

        /// <summary>
        /// 获取部分出库信息
        /// </summary>
        /// <returns></returns>
        public TaskPartDto GetPartOut()
        {
            var _task = taskDataAccess
                .Query<task>(a => 
                a.is_part == EIsPart.Yes.GetHashCode() 
                && a.is_in_stock == EIsInStock.No.GetHashCode() 
                && a.task_type==ETaskType.MoudleOut.GetHashCode())
                .OrderByDescending(a => a.update_time).First();
            var part = mapper.Map<TaskPartDto>(_task);
            if (part==null)
            {
                return null;
            }
            if ((part?.task_type??0)>0)
            {
                part.task_type = ETaskType.MoudleIn.GetHashCode();
            }

            //明细
            var detials = taskDetailBusiness.GetByTaskId(_task.id);
            part.codes = detials?.Select(a => a.fabrication_no)?.ToList();
            return part;
        }

        #region 生成出库任务

        /// <summary>
        /// 生成出库任务
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool, string) CreateMoudleOut(MoudleOutDto t,sys_user user)
        {
            var _stock =stockBusiness.GetStockToOut(t);
            if (_stock == null)
            {
                return (false, "库存信息不存在/不满足出库条件");
            }
            var val = ETaskType.MoudleIn.GetHashCode();
            //部分出库
            if ((t.is_part ?? false) && _stock.task_type != val)
            {
                return (false, "模具支持部分出库");
            }

            var stockDetail = stockDetailBusiness.QueryByStockId(_stock.id);
            //出库任务
            var _task = new task()
            {
                task_no = GetTaskNo(),
                is_in_stock = EIsInStock.No.GetHashCode(),
                task_type = GetOutTaskType(_stock.task_type, t.is_special_out ?? false).GetHashCode(),
                is_part = (t.is_part ?? false) ? 1 : 0,
                material_type = _stock.material_type,
                pallet_barcode = _stock.pallet_barcode,
                roadway_no = _stock.roadway_no,
                start_point = _stock.location_code,
                now_point = _stock.location_code,
                create_id = user.id,
                update_id = user.id
            };
            List<task_detail> taskDetail = new List<task_detail>();
            if (stockDetail?.Count>0)
            {
                var tempDetail = mapper.Map<List<TaskDetailDto>>(stockDetail);
                taskDetail = mapper.Map<List<task_detail>>(tempDetail);
                taskDetail?.ForEach(a =>
                {
                    a.create_id = user.id;
                    a.update_id = user.id;
                    a.state = EState.Use.GetHashCode();
                });
            }
            return excuteHelper.Tran<string>(() =>
            {
                //生成任务
                var add = Insert(_task, taskDetail);
                if (!add.Item1)
                {
                    return add;
                }
                //货位状态出库中
                _stock.location_state = ELocationState.OutStock.GetHashCode();
                //更新货位状态
                if (!stockBusiness.UpdateLocationState(_stock))
                {
                    return (false, "更新货位状态失败");
                }
                return (true, "");
            });
        }

        #endregion

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
            t.sort_no = 0;
            string taskNo = GetTaskNo();
            if (string.IsNullOrWhiteSpace(taskNo))
            {
                return (false, "任务号生成异常");
            }

            t.task_no = taskNo;
            return excuteHelper.Tran(() =>
            {
                var task = taskDataAccess.Insert(t);
                if (task?.id == 0)
                {
                    return (false, "任务创建失败");
                }
                if (details?.Count > 0)
                {
                    details.ForEach(a =>
                    {
                        a.task_id = task.id;
                    });
                    if (taskDetailBusiness.Insert(details) == 0)
                    {
                        return (false, "任务创建明细失败");
                    }
                }
                return (true, "任务创建成功");
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
        /// 任务类型获取货位类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ELocationType GetLocationType(ETaskType type)
        {
            if (type == ETaskType.MoudleSetIn)
            {
                return ELocationType.Big;
            }
            return ELocationType.Common;
        }

        /// <summary>
        /// 写入库存
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ts"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private (bool, string) InsertStock(task t,List<task_detail> ts,sys_user user)
        {
            var dto = mapper.Map<StockDto>(t);
            var _stock = mapper.Map<stock>(dto);
            //入库中，出库完成写入记录
            _stock.location_state = (t.is_in_stock.ToEnum<EIsInStock>() == EIsInStock.Yes ? ELocationState.InStock : ELocationState.Empty).GetHashCode();
            _stock.create_id = user.id;
            _stock.update_id = user.id;
            List<stock_detail> details = new();
            if (ts?.Count>0)
            {
                var dtoList = mapper.Map<List<TaskDetailDto>>(ts);
                details = mapper.Map<List<stock_detail>>(dtoList);
                details.ForEach(a =>
                {
                    a.create_id = user.id;
                    a.update_id = user.id;
                    a.state = EState.Use.GetHashCode();
                });
            }
            var exec = stockBusiness.Insert(_stock);
            if (exec?.id==0)
            {
                return (false, "写入库存失败!");
            }
            if (details?.Count>0)
            {
                details.ForEach(d => d.stock_id = exec.id);
                if (!stockDetailBusiness.Insert(details))
                {
                    return (false, "写入库存明细失败!");
                }
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool UpdateState(task t)
        {
            return taskDataAccess.UpdateColumns(t, a => new { a.task_state,a.update_id,a.update_time},w=>w.id==t.id);
        }

        /// <summary>
        /// 入库转出库类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="is_special_out"></param>
        /// <returns></returns>
        private ETaskType? GetOutTaskType(int type,bool is_special_out)
        {
            if (is_special_out)
            {
                return ETaskType.SpecialOut;
            }
                            
            switch (type.ToEnum<ETaskType>())
            {
                case ETaskType.MoudleIn:
                    return ETaskType.MoudleOut;
                case ETaskType.MoudleSetIn:
                    return ETaskType.MoudleSetOut;
                case ETaskType.OtherIn:
                    return ETaskType.OtherOut;
                case ETaskType.PalletEmptyIn:
                    break;
            }
            return null;
        }

        #endregion

    }
}
