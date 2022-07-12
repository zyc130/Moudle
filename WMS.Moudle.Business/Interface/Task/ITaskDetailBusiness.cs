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

        /// <summary>
        /// 格式化任务数据
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        (bool, string, List<task_detail>) FormatDetails(List<string> codes, sys_user user);
    }
}
