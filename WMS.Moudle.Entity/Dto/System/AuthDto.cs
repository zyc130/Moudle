using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 权限
    /// </summary>
    public class AuthDto
    {
        /// <summary>
        /// 菜单id集合
        /// </summary>
        [Required(ErrorMessage = "菜单集合不能为空")]
        public List<long> menuIds { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [Required(ErrorMessage = "角色id不能为空")]
        public long roleId { get; set; }
    }
}
