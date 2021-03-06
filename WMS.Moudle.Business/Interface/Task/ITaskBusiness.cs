using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Dto.Wcs;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.TaskEnum;

namespace WMS.Moudle.Business.Interface.Task
{
    /// <summary>
    /// 任务
    /// </summary>
    public interface ITaskBusiness
    {
        task Find(long id);

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

        /// <summary>
        /// 获取巷道列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool isSuccess,string msg, List<int> data) GetRoadwayNo(TunnelDto t);

        /// <summary>
        /// 获取分配货位
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool isSuccess, string msg, base_location locat,task _task) GetWareCell(WareCellDto t, sys_user user);

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        (bool,string) Cancel(long id,sys_user user);

        /// <summary>
        /// 手动完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        (bool, string) Finish(long id, sys_user user);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<task> QueryPage(TaskPageDto page);

        /// <summary>
        /// 生成出库任务
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool, string) CreateMoudleOut(MoudleOutDto t,sys_user user);

        /// <summary>
        /// 获取部分出库信息
        /// </summary>
        /// <returns></returns>
        TaskPartDto GetPartOut();
    }
}
