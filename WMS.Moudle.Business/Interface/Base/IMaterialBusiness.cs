using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.Base
{
    /// <summary>
    /// 物料
    /// </summary>
    public interface IMaterialBusiness
    {
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        (bool,string) Import(List<base_material> items);

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code_type"></param>
        /// <returns></returns>
        byte[] Export(string? code, string? code_type);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Add(base_material t);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(base_material t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fabrication_no"></param>
        /// <returns></returns>
        bool Delete(string fabrication_no);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<base_material> QueryPage(MaterialPageDto page);
    }
}
