using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Task;
using WMS.Moudle.DataAccess.Interface.Task;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Serveice.Task
{
    internal class TaskDetailBusiness : ITaskDetailBusiness
    {
        ITaskDetailDataAccess detailDataAccess;
        public TaskDetailBusiness(ITaskDetailDataAccess _detailDataAccess)
        {
            detailDataAccess = _detailDataAccess;
        }

        /// <summary>
        /// 批量插入任务明细
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int Insert(List<task_detail> ts)
        {
           return detailDataAccess.Insert(ts);
        }
    }
}
