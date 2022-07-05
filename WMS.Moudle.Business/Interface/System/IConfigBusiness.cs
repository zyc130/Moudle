using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.System
{
    public interface IConfigBusiness
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool, string, sys_config) Add(sys_config t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(long id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool, string) Update(sys_config t);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        sys_config Find(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<sys_config> FindAll();
    }
}
