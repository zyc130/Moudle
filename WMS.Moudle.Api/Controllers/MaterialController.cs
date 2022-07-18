using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Moudle.Business.Interface.Base;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Api.Controllers
{
    /// <summary>
    /// 物料(摸具)
    /// </summary>
    public class MaterialController : BaseController
    {
        IExcelHelper excelHelper;
        IMaterialBusiness materialBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_excelHelper"></param>
        /// <param name="_materialBusiness"></param>
        /// <param name="_mapper"></param>
        /// <param name="_userBusiness"></param>
        public MaterialController(
            IHttpContextAccessor _httpContextAccessor
            , IExcelHelper _excelHelper
            , IMaterialBusiness _materialBusiness
            , IMapper _mapper
            , IUserBusiness _userBusiness
            , IDictionaryDetailBusiness dictionaryDetailBusiness) : base(_httpContextAccessor, _userBusiness, _mapper,dictionaryDetailBusiness)
        {
            excelHelper = _excelHelper;
            materialBusiness = _materialBusiness;
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Import(IFormCollection file)
        {
            if (file?.Files.Count==0)
            {
                return new ApiResult((false, "导入文件不能为空!"));
            }
            var dataFile = file.Files[0];
            string fileName = dataFile.FileName.ToLower();
            if (!(fileName.EndsWith(".xls")
                || fileName.EndsWith(".xlsx")))
            {
                return new ApiResult((false, "文件只能是Excel类型!"));
            }
            var list = excelHelper.ExcelToList<ImportMaterialDto>(dataFile.OpenReadStream());
            var materials = mapper.Map<List<base_material>>(list);
            materials?.ForEach(a =>
            {
                a.create_time = DateTime.Now;
                a.create_id =user?.id ?? 0;
                a.update_time = DateTime.Now;
                a.update_id = user?.id ?? 0;
                a.state = 1;
            });
            return new ApiResult(materialBusiness.Import(materials));
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="code_type">类型</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Export(string? code,string? code_type)
        {
            var fileByte = materialBusiness.Export(code, code_type);
            return WriteExcel(fileByte,"摸具清单");
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(base_material))]
        public IActionResult QueryPage([FromQuery]MaterialPageDto t)
        {
            var data = materialBusiness.QueryPage(t);
            data?.DataList.SetName(userBusiness.FindAll());
            return new ApiResult(data);
        }
    }
}
