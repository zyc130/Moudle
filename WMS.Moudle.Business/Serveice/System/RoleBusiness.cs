using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.DataAccess.Interface.System;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Business.Serveice.System
{
    internal class RoleBusiness : IRoleBusiness
    {
        IRoleDataAccess roleDataAccess;
        IUserBusiness userBusiness;
        IExcuteHelper exuteHelper;
        IRoleMenuBusiness roleMenuBusiness;

        public RoleBusiness(
            IRoleDataAccess _roleDataAccess
            , IUserBusiness _userBusiness
            , IExcuteHelper _exuteHelper
            , IRoleMenuBusiness _roleMenuBusiness)
        {
            roleDataAccess = _roleDataAccess;
            userBusiness = _userBusiness;
            exuteHelper = _exuteHelper;
            roleMenuBusiness = _roleMenuBusiness;
        }

        public (bool isSuccess, string msg, sys_role t) Add(sys_role t)
        {
            var items = FindAll();
            if (items?.Exists(a => a.code.Equals(t.code)) ?? false)
            {
                return new(false, "编号已存在", null);
            }
            if (items?.Exists(a => a.name.Equals(t.name)) ?? false)
            {
                return new(false, "名称已存在", null);
            }
            t.create_time = DateTime.Now;
            t.update_time = DateTime.Now;
            return new(true, string.Empty, roleDataAccess.Insert(t));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (bool,string) Delete(long id)
        {
            var role = Find(id);
            if (role==null)
            {
                return new(false, "角色不存在!");
            }
            return exuteHelper.Tran<string>(() =>
            {
                //清空用户关联
                if (!userBusiness.RemoveRoleId(id))
                {
                    return new(false, "清空用户角色失败!");
                }
                //清空权限分配
                if (!roleMenuBusiness.DeleteByRoleId(id))
                {
                    return new(false, "清空角色权限失败!");
                }
                //删除角色
                if (!roleDataAccess.Delete<sys_role>(id))
                {
                    return new(false, "删除角色失败!");
                }
                return new(true, "");
            });
        }

        public sys_role Find(long id)
        {
            return roleDataAccess.Find<sys_role>(id);
        }

        public List<sys_role> FindAll()
        {
            return roleDataAccess.FindAll<sys_role>();
        }

        public PageData<sys_role> QueryPage(SystemPageDto page)
        {
            Expressionable<sys_role> express = new();
            express.AndIF(!string.IsNullOrWhiteSpace(page.code), a => a.code.Contains(page.code))
                .AndIF(!string.IsNullOrWhiteSpace(page.name), a => a.name.Contains(page.name));
            return roleDataAccess.QueryPage(page.pageIndex, page.pageSize, express.ToExpression(), a => a.update_time, false);
        }

        public bool Update(sys_role t)
        {
            var role = Find(t.id);
            if (role == null)
            {
                return false;
            }
            t.update_time = DateTime.Now;
            return roleDataAccess.UpdateIgnore(t, a => new { a.create_time, a.create_id });
        }
    }
}
