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
        public TaskBusiness(ITaskDataAccess _taskDataAccess
            , IMapper _mapper
            , IExcuteHelper _excuteHelper
            , ITaskDetailBusiness _taskDetailBusiness)
        {
            taskDataAccess = _taskDataAccess;
            mapper = _mapper;
            excuteHelper = _excuteHelper;
            taskDetailBusiness = _taskDetailBusiness;
        }

        public (bool, string) CreateMoudleIn(MoudleInDto t, long userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 托盘入库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool, string) CreatePalletIn(task t)
        {
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
        /// 创建任务
        /// </summary>
        /// <param name="t"></param>
        /// <param name="detials"></param>
        /// <returns></returns>
        private (bool, string) Insert(task t, List<task_detail> details)
        {
            t.state = CommonEnum.EState.Use.GetHashCode();
            t.task_state = TaskEnum.ETaskState.Wait.GetHashCode();
            return excuteHelper.Tran<string>(() =>
            {
                var task = taskDataAccess.Insert<task>(t);
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


    }
}
