﻿using AutoMapper;
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

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_taskBusiness"></param>
        public TaskController(IHttpContextAccessor _httpContextAccessor, IUserBusiness _userBusiness, IMapper _mapper
            , ITaskBusiness _taskBusiness) : base(_httpContextAccessor, _userBusiness, _mapper)
        {
            taskBusiness = _taskBusiness;
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
                items.Add(new task()
                {
                    material_type = t.material_type.GetHashCode(),
                    is_in_stock = TaskEnum.EIsInStock.No.GetHashCode(),
                    task_type = TaskEnum.ETaskType.PalletEmptyOut.GetHashCode(),
                    create_id = user?.id ?? 0,
                    update_id = user?.id ?? 0,
                    state = CommonEnum.EState.Use.GetHashCode(),
                    task_state = TaskEnum.ETaskState.Wait.GetHashCode()
                });
            }
            return new ApiResult(taskBusiness.CreatePalletOut(items));
        }
        #endregion

        #region 模具入库

        /// <summary>
        /// 模具入库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateMoudleIn(MoudleInDto t)
        {
            return new ApiResult(taskBusiness.CreateMoudleIn(t,user?.id??0));
        }

        #endregion

    }
}