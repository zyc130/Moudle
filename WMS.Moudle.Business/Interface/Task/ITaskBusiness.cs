using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.Task
{
    public interface ITaskBusiness
    {
        /// <summary>
        /// 空托盘入库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool, string) CreatePalletIn(task t);

        /// <summary>
        /// 空托盘出库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool CreatePalletOut(List<task> ts);

        /// <summary>
        /// 模具入库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool, string) CreateMoudleIn(MoudleInDto t,long userId);

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        task Insert(task t);
    }
}
