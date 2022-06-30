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
    /// 菜单按钮
    /// </summary>
    public interface IMenuBusiness
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public sys_menu Add(sys_menu t);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (bool,string) Delete(long id);

        /// <summary>
        /// Udpate
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Udpate(sys_menu t);

        /// <summary>
        /// FindAll
        /// </summary>
        /// <returns></returns>
        public List<sys_menu> FindAll();

        /// <summary>
        /// 树结构
        /// </summary>
        /// <returns></returns>
        public List<MenuTreeDto> QueryTree();
    }
}
