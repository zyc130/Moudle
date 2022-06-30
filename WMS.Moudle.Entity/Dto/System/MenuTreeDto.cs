using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 菜单树
    /// </summary>
    public class MenuTreeDto
    {
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long id { get; set; }

        /// <summary>
        /// Desc:上一级id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? parent_id { get; set; }

        /// <summary>
        /// Desc:菜单名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string name { get; set; }

        /// <summary>
        /// Desc:地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string url { get; set; }

        /// <summary>
        /// Desc:icon地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string img { get; set; }

        /// <summary>
        /// Desc:操作按钮标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string action_key { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? order_no { get; set; }

        /// <summary>
        /// Desc:菜单级别
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int menu_level { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<MenuTreeDto> child { get; set; }
    }
}
