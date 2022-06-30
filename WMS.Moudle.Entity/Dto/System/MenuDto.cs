using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 菜单按钮dto
    /// </summary>
    public class MenuDto
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
        [Required(ErrorMessage = "父级id不能为空")]
        public long? parent_id { get; set; }

        /// <summary>
        /// Desc:菜单名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "菜单名称不能为空")]
        [MaxLength(20, ErrorMessage = "菜单名称最大长度:20")]
        public string name { get; set; }

        /// <summary>
        /// Desc:地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        [MaxLength(200, ErrorMessage = "url最大长度:200")]
        public string url { get; set; }

        /// <summary>
        /// Desc:icon地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        [MaxLength(200, ErrorMessage = "img最大长度:200")]
        public string img { get; set; }

        /// <summary>
        /// Desc:操作按钮标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        [MaxLength(50, ErrorMessage = "按钮标识最大长度:50")]
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
        [Required(ErrorMessage = "菜单级别不能为空")]
        public int menu_level { get; set; }

        /// <summary>
        /// Desc:状态(1-启用，0-停用)
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "状态不能为空")]
        [Range(minimum:0,maximum:1,ErrorMessage = "状态值为:0/1")]
        public int state { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        [MaxLength(100, ErrorMessage = "按钮标识最大长度:100")]
        public string remark { get; set; }
    }
}
