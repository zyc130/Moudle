using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.Wcs;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// wcs交互接口
    /// </summary>
    public class WCSController : BaseController
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        public WCSController(IHttpContextAccessor _httpContextAccessor, IUserBusiness _userBusiness, IMapper _mapper) : base(_httpContextAccessor, _userBusiness, _mapper)
        {
        }

        /// <summary>
        /// 获取巷道
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResResult I_WCS_GetTunnelList(TunnelDto t)
        {
            return new ResResult(true,"",t);
        }

        /// <summary>
        /// 获取货位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResResult I_WCS_GetWareCell(WareCellDto t)
        {
            return new ResResult(true, "", t);
        }

        /// <summary>
        /// 返回对象
        /// </summary>
        public class ResResult
        {
            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="result"></param>
            /// <param name="msg"></param>
            /// <param name="data"></param>
            public ResResult(bool result,string msg,object data)
            {
                ResType=result; 
                ResMessage=msg;
                ResData=data;
            }

            /// <summary>
            /// 是否成功
            /// </summary>
            public bool ResType { get; set; }

            /// <summary>
            /// 提示信息
            /// </summary>
            public string ResMessage { get; set; }

            /// <summary>
            /// 数据
            /// </summary>
            public object ResData { get; set; }
        }
    }
}
