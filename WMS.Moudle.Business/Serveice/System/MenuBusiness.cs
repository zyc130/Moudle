using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.DataAccess.Interface.System;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Serveice.System
{
    internal class MenuBusiness : IMenuBusiness
    {
        IMenuDataAccess menuDataAccess;
        IMapper mapper;
        public MenuBusiness(IMenuDataAccess _menuDataAccess
             ,IMapper _mapper)
        {
            menuDataAccess = _menuDataAccess;
            mapper = _mapper;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public sys_menu Add(sys_menu t)
        {
            t.update_time = DateTime.Now;
            t.create_time = DateTime.Now;
            return menuDataAccess.Insert(t);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (bool,string) Delete(long id)
        {
            return new(false, "删除功能暂未开发!");
            //子菜单一起删除
            //return menuDataAccess.Delete<sys_menu>(id);
        }

        /// <summary>
        /// FindAll
        /// </summary>
        /// <returns></returns>
        public List<sys_menu> FindAll()
        {
            return menuDataAccess.FindAll<sys_menu>();
        }

        public List<MenuTreeDto> QueryTree()
        {
            var items = FindAll();
            List<MenuTreeDto> trees = new List<MenuTreeDto>();
            FormatTree(items, trees, new MenuTreeDto() { id=0,parent_id=0});
            return trees;
        }

        /// <summary>
        /// Udpate
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Udpate(sys_menu t)
        {
            var menu = menuDataAccess.Find<sys_menu>(t.id);
            if (menu == null)
            {
                return false;
            }
            t.update_time = DateTime.Now;
            return menuDataAccess.UpdateIgnore(t, u => new { u.create_id,u.create_time});
        }

        #region private

        /// <summary>
        /// 获取树结构
        /// </summary>
        /// <param name="data"></param>
        /// <param name="result"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        private List<MenuTreeDto> FormatTree(List<sys_menu> data, List<MenuTreeDto> result, MenuTreeDto index)
        {
            var _data = data?.FindAll(a => a.parent_id == index.id)?.OrderBy(a => a.order_no).ToList();
            List<MenuTreeDto> _child = new List<MenuTreeDto>();
            _data?.ForEach(a =>
            {
                var menu = mapper.Map<sys_menu, MenuTreeDto>(a);
                menu.child = new List<MenuTreeDto>();
                if (index.id == 0)
                {
                    result.Add(menu);
                }
                else
                {
                    _child.Add(menu);
                }
                if (data?.Exists(d => d.parent_id == a.id) ?? false)
                {
                    menu.child = FormatTree(data, result, menu);
                }
            });
            return _child ?? new List<MenuTreeDto>();
        }

        #endregion
    }
}
