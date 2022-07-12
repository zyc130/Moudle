﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.TaskEnum;

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
        (bool, string) CreateMoudleIn(MoudleInDto t, sys_user user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool, string, int) QueryMoudleCount(MoudleCountDto t);

        /// <summary>
        /// 获取任务号当天索引
        /// </summary>
        /// <returns></returns>
        string GetTaskNo();

        /// <summary>
        /// 获取全部等待中的任务
        /// </summary>
        /// <returns></returns>
        List<task> GetWaitAll();

        /// <summary>
        /// 可用货位数(包括等待中任务)
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        int GetLocationEmptyCount(ETaskType taskType);
    }
}
