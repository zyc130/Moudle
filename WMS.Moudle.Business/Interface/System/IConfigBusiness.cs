using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.CommonEnum;

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
        /// 全部
        /// </summary>
        /// <returns></returns>
        List<sys_config> FindAll();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<sys_config> QueryPage(ConfigPageDto page);

        /// <summary>
        /// 获取配置value值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int? QueryValue(EConfigCode code);
    }
}
