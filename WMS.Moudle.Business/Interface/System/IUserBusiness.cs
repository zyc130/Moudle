using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Interface.System
{
    public interface IUserBusiness
    {
        /// <summary>
        /// 根据用户名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        sys_user QueryByName(string name);

        /// <summary>
        /// 根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        sys_user Find(long id);

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        List<sys_user> FindAll();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool isSuccess, string msg, sys_user t)  Add(sys_user t);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        (bool,string) Update(sys_user t);

        /// <summary>
        /// 清空角色关联
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool RemoveRoleId(long roleId);

        /// <summary>
        /// 清空部门关联
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        bool RemoveDeptId(long deptId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(long id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageData<sys_user> QueryPage(SystemPageDto page);
    }
}
