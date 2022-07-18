using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WMS.Moudle.Business.Interface.Stock;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Business.Interface.Task;
using WMS.Moudle.Entity.Dto.Wcs;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// wcs交互接口
    /// </summary>
    public class WCSController : BaseController
    {
        ITaskBusiness taskBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_taskBusiness"></param>
        public WCSController(IHttpContextAccessor _httpContextAccessor
            , IUserBusiness _userBusiness
            , IMapper _mapper
            , ITaskBusiness _taskBusiness
            , IDictionaryDetailBusiness dictionaryDetailBusiness
            ) : base(_httpContextAccessor, _userBusiness, _mapper,dictionaryDetailBusiness)
        {
            taskBusiness = _taskBusiness;
        }

        /// <summary>
        /// 获取巷道
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResResult I_WCS_GetTunnelList(TunnelDto t)
        {
            var (isSuccess, msg, data) = taskBusiness.GetRoadwayNo(t);
            return new ResResult(isSuccess, msg
                , new { SrmCurTunnelList=data });
        }

        /// <summary>
        /// 获取货位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResResult I_WCS_GetWareCell(WareCellDto t)
        {
            var (isSuccess, msg, locat, _task) = taskBusiness.GetWareCell(t, user);
            JObject obj = new();
            if (isSuccess)
            {
                obj = new JObject()
                {
                    {"WMSTaskNum",t.WMSTaskNum },
                    { "TaskType",_task.task_type},
                    { "CellNo",locat.location_code},
                    { "Row",locat.row_no},
                    { "Colomn",locat.column_no},
                    { "Layer",locat.floor_no},
                    { "TunnelNum",locat.roadway_no}
                };
            }
            return new ResResult(isSuccess, msg, obj);
        }

    }
}
