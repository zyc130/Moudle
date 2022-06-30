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
    /// 角色
    /// </summary>
    public interface IRoleBusiness
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        sys_role Find(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<sys_role> FindAll();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        (bool,string) Delete(long id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool isSuccess, string msg, sys_role t) Add(sys_role t);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(sys_role t);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<sys_role> QueryPage(SystemPageDto page);
    }
}
