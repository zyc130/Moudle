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
    /// 字典键值
    /// </summary>
    public interface IDictionaryDetailBusiness
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool,string,sys_dictionary_detail?) Add(sys_dictionary_detail t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(long id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool DeleteByCode(string code);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool,string) Update(sys_dictionary_detail t);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool UpdateByCode(sys_dictionary_detail t,string oldDicCode);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        sys_dictionary_detail Find(long id);

        /// <summary>
        /// 根据字典类型获取列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<sys_dictionary_detail> FindByCode(string code);

        #region option

        /// <summary>
        /// 托盘类型
        /// </summary>
        /// <returns></returns>
        List<sys_dictionary_detail> GetPalletType();

        /// <summary>
        /// 模具入库类型
        /// </summary>
        /// <returns></returns>
        List<sys_dictionary_detail> GetMoudleInType();

        /// <summary>
        /// 模具类型
        /// </summary>
        /// <returns></returns>
        List<sys_dictionary_detail> GetMoudleType();

        #endregion
    }
}
