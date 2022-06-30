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
    internal partial class DeptBusiness : IDeptBusiness
    {
        IDeptDataAccess deptDataAccess;
        IUserBusiness userBusiness;
        IExcuteHelper exuteHelper;

        public DeptBusiness(
            IDeptDataAccess _deptDataAccess
            ,IUserBusiness _userBusiness
            ,IExcuteHelper _exuteHelper)
        {
            deptDataAccess = _deptDataAccess;
            userBusiness = _userBusiness;
            exuteHelper = _exuteHelper;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool, string, sys_dept) Add(sys_dept t)
        {
            var items = deptDataAccess.FindAll<sys_dept>();
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
            return new(true, string.Empty, deptDataAccess.Insert(t));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (bool,string) Delete(long id)
        {
            var dept = Find(id);
            if (dept == null)
            {
                return new(false,"部门不存在!");
            }
            return exuteHelper.Tran<string>(() =>
            {
                if(!userBusiness.RemoveDeptId(id))
                {
                    return new(false, "清空用户部门失败!");
                }
                if (!deptDataAccess.Delete<sys_dept>(id))
                {
                    return new(false, "删除部门失败");
                }
                return new(true, "");
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public sys_dept Find(long id)
        {
            return deptDataAccess.Find<sys_dept>(id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<sys_dept> FindAll()
        {
            return deptDataAccess.FindAll<sys_dept>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PageData<sys_dept> QueryPage(SystemPageDto page)
        {
            Expressionable<sys_dept> express = new Expressionable<sys_dept>();
            express.AndIF(!string.IsNullOrWhiteSpace(page.code), a => a.code.Contains(page.code))
                .AndIF(!string.IsNullOrWhiteSpace(page.name), a => a.name.Contains(page.name));
            return deptDataAccess.QueryPage(page.pageIndex, page.pageSize, express.ToExpression(), a => a.update_time, false);
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(sys_dept t)
        {
            var dept = deptDataAccess.Find<sys_dept>(t.id);
            if (dept == null)
            {
                return false;
            }
            t.update_time = DateTime.Now;
            return deptDataAccess.UpdateIgnore(t, a => new { a.create_time,a.create_id});
        }
    }
}
