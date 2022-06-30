using SqlSugar;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.DataAccess.Interface.System;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Serveice.System
{
    internal class UserBusiness : IUserBusiness
    {
        IUserDataAccess userDataAccess; 
        public UserBusiness(IUserDataAccess _userDataAccess) 
        { 
            userDataAccess = _userDataAccess;
        }

        public bool Delete(long id)
        {
            return userDataAccess.Delete<sys_user>(id);
        }

        public sys_user Find(long id)
        {
            return userDataAccess.Find<sys_user>(id);
        }

        public List<sys_user> FindAll()
        {
            return userDataAccess.FindAll<sys_user>();
        }

        public (bool isSuccess, string msg, sys_user t) Add(sys_user t)
        {
            var items = FindAll();
            if (items?.Exists(a => a.code.Equals(t.code)) ?? false)
            {
                return new(false, "编号已存在", null);
            }
            if (items?.Exists(a => a.name.Equals(t.name)) ?? false)
            {
                return new(false, "用户名已存在", null);
            }
            t.create_time = DateTime.Now;
            t.update_time = DateTime.Now;
            return new(true, string.Empty, userDataAccess.Insert(t));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public sys_user QueryByName(string name)
        {
            return userDataAccess.Query<sys_user>(a => a.name.Equals(name))?.First();
        }

        public PageData<sys_user> QueryPage(SystemPageDto page)
        {
            Expressionable<sys_user> express = new();
            express.AndIF(!string.IsNullOrWhiteSpace(page.code), a => a.code.Contains(page.code))
                .AndIF(!string.IsNullOrWhiteSpace(page.name), a => a.name.Contains(page.name));
            return userDataAccess.QueryPage(page.pageIndex, page.pageSize, express.ToExpression(), a => a.update_time, false);
        }

        public (bool,string) Update(sys_user t)
        {
            var user = userDataAccess.Find<sys_user>(t.id);
            if (user == null)
            {
                return new(false,"对象不存在!");
            }
            var items = FindAll();
            if (items?.Exists(a => a.code.Equals(t.code) && a.id!=t.id) ?? false)
            {
                return new(false, "编号已存在!");
            }
            t.update_time = DateTime.Now;
            if (!userDataAccess.UpdateIgnore(t, a => new { a.create_time, a.create_id, a.name }))
            {
                return new(false, "更新失败!");
            }
            return new(true, "");
        }

        /// <summary>
        /// 清空角色关联
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool RemoveRoleId(long roleId)
        {
            var users = userDataAccess.Query<sys_user>(a => a.role_id == roleId);
            if (users?.Count() > 0)
            {
                var user = new sys_user() { role_id = 0 };
                return userDataAccess.UpdateColumns(user, a => new { a.role_id },w=>w.role_id==roleId);
            }
            return true;
        }

        /// <summary>
        /// 清空部门关联
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool RemoveDeptId(long deptId)
        {
            var users = userDataAccess.Query<sys_user>(a => a.dept_id == deptId);
            if (users?.Count() > 0)
            {
                var user = new sys_user() { dept_id = 0 };
                return userDataAccess.UpdateColumns(user, a => new { a.dept_id },w=>w.dept_id==deptId);
            }
            return true;
        }
    }
}
