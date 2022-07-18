using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Business.Interface.Task;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Enum;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 任务
    /// </summary>
    public class TaskController : BaseController
    {
        ITaskBusiness taskBusiness;
        ITaskDetailBusiness taskDetailBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_taskBusiness"></param>
        /// <param name="_taskDetailBusiness"></param>
        public TaskController(
            IHttpContextAccessor _httpContextAccessor
            , IUserBusiness _userBusiness
            , IMapper _mapper
            , IDictionaryDetailBusiness dictionaryDetailBusiness
            , ITaskBusiness _taskBusiness
            , ITaskDetailBusiness _taskDetailBusiness) 
            : base(_httpContextAccessor, _userBusiness, _mapper, dictionaryDetailBusiness)
        {
            taskBusiness = _taskBusiness;
            taskDetailBusiness = _taskDetailBusiness;
        }

        #region 空托盘出入库

        /// <summary>
        /// 空托盘入库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePalletIn([FromBody]PalletInDto t)
        {
            var _task = new task()
            {
                material_type=t.material_type.GetHashCode(),
                is_in_stock= TaskEnum.EIsInStock.Yes.GetHashCode(),
                task_type = TaskEnum.ETaskType.PalletEmptyIn.GetHashCode(),
                create_id=user?.id??0,
                update_id=user?.id??0
            };
            return new ApiResult(taskBusiness.CreatePalletIn(_task));
        }

        /// <summary>
        /// 空托盘出库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePalletOut([FromBody] PalletOutDto t)
        {
            List<task> items = new();
            for (int i = 0; i < t.number; i++)
            {
                string taskNo = taskBusiness.GetTaskNo();
                if (string.IsNullOrWhiteSpace(taskNo))
                {
                    return new ApiResult((false, "任务号生成异常!"));
                }
                items.Add(new task()
                {
                    material_type = t.material_type.GetHashCode(),
                    is_in_stock = TaskEnum.EIsInStock.No.GetHashCode(),
                    task_type = TaskEnum.ETaskType.PalletEmptyOut.GetHashCode(),
                    create_id = user?.id ?? 0,
                    update_id = user?.id ?? 0,
                    state = CommonEnum.EState.Use.GetHashCode(),
                    task_state = TaskEnum.ETaskState.Wait.GetHashCode(),
                    task_no = taskNo
                });
            }
            return new ApiResult(taskBusiness.CreatePalletOut(items));
        }
        #endregion

        #region 模具入库

        /// <summary>
        /// 获取入库条码部件数量
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult QueryMoudleCount([FromQuery]MoudleCountDto t)
        {
            return new ApiResult(taskBusiness.QueryMoudleCount(t));
        }

        /// <summary>
        /// 模具入库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateMoudleIn(MoudleInDto t)
        {
            return new ApiResult(taskBusiness.CreateMoudleIn(t,user));
        }

        #endregion

        #region 出库

        /// <summary>
        /// 生成出库任务
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateMoudleOut([FromBody]MoudleOutDto t)
        {
            return new ApiResult(taskBusiness.CreateMoudleOut(t,user));
        }
        #endregion

        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Cancel(long id)
        {
            return new ApiResult(taskBusiness.Cancel(id, user));
        }

        /// <summary>
        /// 手动完成
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Finish(long id)
        {
            return new ApiResult(taskBusiness.Finish(id, user));
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(task))]
        public IActionResult QueryPage([FromQuery] TaskPageDto page)
        {
            var data = taskBusiness.QueryPage(page);
            data?.DataList.SetName(userBusiness.FindAll())
                .ToDictionaryName(dictionaryDetailBusiness);
            return new ApiResult(data);
        }

        /// <summary>
        /// 任务详情
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(task_detail))]
        public IActionResult QueryByDetail(long taskId)
        {
            return new ApiResult(taskDetailBusiness.GetByTaskId(taskId));
        }

        /// <summary>
        /// 部分回库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskPartDto))]
        public IActionResult QueryPartIn()
        {
            var data = taskBusiness.GetPartOut();
            data?.details?.ToDictionaryName(dictionaryDetailBusiness);
            return new ApiResult(data);
        }
    }
}
