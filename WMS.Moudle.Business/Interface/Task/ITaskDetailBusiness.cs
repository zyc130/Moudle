using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.Task
{
    public interface ITaskDetailBusiness
    {
        /// <summary>
        /// 批量插入任务明细
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        int Insert(List<task_detail> ts);
    }
}
