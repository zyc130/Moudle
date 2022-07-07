using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.System
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public interface IDictionaryBusiness
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool, string, sys_dictionary?) Add(sys_dictionary t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        (bool,string) Delete(long id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool,string) Update(sys_dictionary t);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        sys_dictionary Find(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<sys_dictionary> FindAll();


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<sys_dictionary> QueryPage(SystemPageDto page);
    }
}
