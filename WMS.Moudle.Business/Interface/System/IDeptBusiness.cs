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
    public interface IDeptBusiness
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        sys_dept Find(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<sys_dept> FindAll();

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
        (bool isSuccess, string msg, sys_dept t) Add(sys_dept t);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(sys_dept t);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<sys_dept> QueryPage(SystemPageDto page);
    }
}
