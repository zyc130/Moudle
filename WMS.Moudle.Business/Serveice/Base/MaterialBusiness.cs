using AutoMapper;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Base;
using WMS.Moudle.DataAccess.Interface.Base;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Business.Serveice.Base
{
    internal class MaterialBusiness : IMaterialBusiness
    {
        IExcelHelper excelHelper;
        IExcuteHelper excuteHelper;
        IMaterialDataAccess materialDataAccess;
        IMapper mapper;
        public MaterialBusiness(
            IExcelHelper _excelHelper,
            IMaterialDataAccess _materialDataAccess,
            IExcuteHelper excuteHelper,
            IMapper _mapper
                    )
        {
            excelHelper = _excelHelper;
            materialDataAccess = _materialDataAccess;
            this.excuteHelper = excuteHelper;
            mapper = _mapper;
        }
        public bool Add(base_material t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fabrication_no"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(string fabrication_no)
        {
            return materialDataAccess.Delete<base_material>(a => a.fabrication_no == fabrication_no);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code_type"></param>
        public byte[] Export(string? code,string? code_type)
        {
            Expressionable<base_material> express = new();
            express.AndIF(!string.IsNullOrWhiteSpace(code), a => a.fabrication_no.Contains(code))
                .AndIF(!string.IsNullOrWhiteSpace(code_type), a => a.piece_code.Contains(code_type));
            var items = materialDataAccess.Query(express.ToExpression());
            var exportData = mapper.Map<List<ImportMaterialDto>>(items?.ToList());
           return excelHelper.Export(exportData);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool,string) Import(List<base_material> items)
        {
            if (items?.Count==0)
            {
                return (false, "数据不能为空!");
            }
            return excuteHelper.Tran<string>(() =>
            {
                if (materialDataAccess.Count()>0)
                {
                    //删除全部
                    if (!materialDataAccess.Delete<base_material>(a => a.fabrication_no == a.fabrication_no))
                    {
                        return (false, "删除失败!");
                    }
                }
                //插入
                if (materialDataAccess.Insert(items)==0)
                {
                    return (false, "导入失败!");
                }
                return (true, $"导入成功，共:{items?.Count}条");
            });
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PageData<base_material> QueryPage(MaterialPageDto page)
        {
            Expressionable<base_material> express = new();
            express.AndIF(!string.IsNullOrWhiteSpace(page.code), a => a.fabrication_no.Contains(page.code))
                .AndIF(!string.IsNullOrWhiteSpace(page.code_type), a => a.piece_code.Contains(page.code_type));
            return materialDataAccess.QueryPage(page.pageIndex, page.pageSize, express.ToExpression(), a => a.create_time, false);
        }

        public bool Update(base_material t)
        {
            throw new NotImplementedException();
        }
    }
}
