using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.DataAccess.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Business.Serveice.System
{
    /// <summary>
    /// 角色权限
    /// </summary>
    internal class RoleMenuBusiness : IRoleMenuBusiness
    {
        IRoleMenuDataAccess roleMenuDataAccess;
        IExcuteHelper exuteHelper;

        public RoleMenuBusiness(
            IRoleMenuDataAccess _roleMenuDataAccess
            , IExcuteHelper _exuteHelper)
        {
            roleMenuDataAccess= _roleMenuDataAccess;
            exuteHelper= _exuteHelper;
        }

        /// <summary>
        /// 根据菜单id清空用户信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteByMenuId(long menuId)
        {
            var items = roleMenuDataAccess.Query<sys_role_menu>(a => a.menu_id == menuId);
            if (items?.Count()==0)
            {
                return true;
            }
            return roleMenuDataAccess.Delete<sys_role_menu>(a => a.menu_id == menuId);
        }

        /// <summary>
        /// 根据角色id清空用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteByRoleId(long roleId)
        {
            var items = roleMenuDataAccess.Query<sys_role_menu>(a => a.role_id == roleId);
            if (items?.Count() == 0)
            {
                return true;
            }
            return roleMenuDataAccess.Delete<sys_role_menu>(a => a.role_id == roleId);
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<sys_role_menu> FindByRoleId(long roleId)
        {
            return roleMenuDataAccess.Query<sys_role_menu>(a => a.role_id == roleId)?.ToList()??new();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public (bool,string) Save(AuthDto t)
        {
            //执行保存
            return exuteHelper.Tran<string>(() =>
            {
                if (FindByRoleId(t.roleId)?.Count>0)
                {
                    //删除原数据
                    if (!roleMenuDataAccess.Delete<sys_role_menu>(a => a.role_id == t.roleId))
                    {
                        return new(true, "数据清空失败!");
                    }
                }
                //保存新数据
                List<sys_role_menu> roleMenus = new();
                t.menuIds?.Distinct().ToList().ForEach(a =>
                {
                    if (a > 0)
                    {
                        roleMenus.Add(new sys_role_menu() { menu_id = a, role_id = t.roleId });
                    }
                });
                if (roleMenuDataAccess.Insert(roleMenus)==0)
                {
                    return new(true, "保存失败!");
                }
                return new(true, "");
            });
        }
    }
}
