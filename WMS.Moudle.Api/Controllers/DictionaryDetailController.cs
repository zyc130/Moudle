﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 字典键值
    /// </summary>
    public class DictionaryDetailController : BaseController
    {
        IDictionaryDetailBusiness dictionaryDetailBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_userBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="dictionaryDetailBusiness"></param>
        public DictionaryDetailController(IHttpContextAccessor _httpContextAccessor, IUserBusiness _userBusiness, IMapper _mapper
            , IDictionaryDetailBusiness dictionaryDetailBusiness) : base(_httpContextAccessor, _userBusiness, _mapper)
        {
            this.dictionaryDetailBusiness = dictionaryDetailBusiness;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(DicDetailDto t)
        {
            var dic = mapper.Map<DicDetailDto, sys_dictionary_detail>(t);
            dic.create_id = user?.id ?? 0;
            dic.update_id = user?.id ?? 0;
            return new ApiResult(dictionaryDetailBusiness.Add(dic));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Query(long id)
        {
            return new ApiResult(dictionaryDetailBusiness.Find(id));
        }


        /// <summary>
        /// 根据类型查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryByCode(string dicCode)
        {
            return new ApiResult(dictionaryDetailBusiness.FindByCode(dicCode));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return new ApiResult(dictionaryDetailBusiness.Delete(id));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(DicDetailDto t)
        {
            var dic = mapper.Map<DicDetailDto, sys_dictionary_detail>(t);
            dic.update_id = user?.id ?? 0;
            return new ApiResult(dictionaryDetailBusiness.Update(dic));
        }
    }
}