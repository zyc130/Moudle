using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class ConfigController : BaseController
    {
        IConfigBusiness configBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="configBusiness"></param>
        public ConfigController(IHttpContextAccessor _httpContextAccessor, IUserBusiness _userBusiness, IMapper _mapper, IConfigBusiness configBusiness, IDictionaryDetailBusiness dictionaryDetailBusiness) : base(_httpContextAccessor, _userBusiness, _mapper,dictionaryDetailBusiness)
        {
            this.configBusiness = configBusiness;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_config))]
        public IActionResult Add(sys_config t)
        {
            t.create_id = user?.id ?? 0;
            t.update_id = user?.id ?? 0;
            return new ApiResult(configBusiness.Add(t));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_config))]
        public IActionResult Query(long id)
        {
            return new ApiResult(configBusiness.Find(id));
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_config))]
        public IActionResult QueryPage([FromQuery]ConfigPageDto page)
        {
            var data = configBusiness.QueryPage(page);
            data.DataList?.SetName(userBusiness.FindAll())
                .ToDictionaryName(dictionaryDetailBusiness);
            return new ApiResult(data);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return new ApiResult(configBusiness.Delete(id));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(ConfigDto t)
        {
            var config = mapper.Map<ConfigDto, sys_config>(t);
            config.update_id = user?.id ?? 0;
            return new ApiResult(configBusiness.Update(config));
        }
    }
}
