using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.Base
{
    /// <summary>
    /// 库位表
    /// </summary>
    public interface ILocationBusiness
    {
        /// <summary>
        /// 初始化货位
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="createId"></param>
        /// <returns></returns>
        (bool,string) Init(List<LocationInitDto> ts, long createId);

        /// <summary>
        /// 获取货架信息
        /// </summary>
        /// <returns></returns>
        List<LocationShowDto> QueryAll();

        /// <summary>
        /// 获取货架行数
        /// </summary>
        /// <returns></returns>
        List<int> QueryRows();

        /// <summary>
        /// 根据行号获取货位
        /// </summary>
        /// <returns></returns>
        LocationShowDto QueryByRowNo(int rowNo);
    }
}
