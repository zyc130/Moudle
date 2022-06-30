using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.System
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public interface IRoleMenuBusiness
    {
        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<sys_role_menu> FindByRoleId(long roleId);

        /// <summary>
        /// 根据角色id清空用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool DeleteByRoleId(long roleId);

        /// <summary>
        /// 根据菜单id清空用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool DeleteByMenuId(long menuId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool isSuccess,string msg) Save(AuthDto t);
    }
}
