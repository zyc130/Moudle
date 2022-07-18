using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 字典分类
    /// </summary>
    public class DictionaryController : BaseController
    {
        IDictionaryBusiness dictionaryBusiness;

        /// <summary>
        /// 字典类型
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_dictionary"></param>
        public DictionaryController(
            IHttpContextAccessor _httpContextAccessor
            , IUserBusiness _userBusiness
            , IMapper _mapper
            , IDictionaryBusiness _dictionary
            , IDictionaryDetailBusiness dictionaryDetailBusiness) 
            : base(_httpContextAccessor, _userBusiness, _mapper, dictionaryDetailBusiness)
        {
            dictionaryBusiness = _dictionary;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dictionary))]
        public IActionResult Add(SystemDto t)
        {
            var dic = mapper.Map<SystemDto, sys_dictionary>(t);
            dic.create_id = user?.id ?? 0;
            dic.update_id = user?.id ?? 0;
            return new ApiResult(dictionaryBusiness.Add(dic));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dictionary))]
        public IActionResult Query(long id)
        {
            return new ApiResult(dictionaryBusiness.Find(id));
        }


        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dictionary))]
        public IActionResult QueryAll()
        {
            return new ApiResult(dictionaryBusiness.FindAll());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return new ApiResult(dictionaryBusiness.Delete(id));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(SystemDto t)
        {
            var dic = mapper.Map<SystemDto, sys_dictionary>(t);
            dic.update_id = user?.id ?? 0;
            return new ApiResult(dictionaryBusiness.Update(dic));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sys_dictionary))]
        public IActionResult QueryPage([FromQuery] SystemPageDto t)
        {
            var data = dictionaryBusiness.QueryPage(t);
            data?.DataList.SetName(userBusiness.FindAll());
            return new ApiResult(data);
        }
    }
}
