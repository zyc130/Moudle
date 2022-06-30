using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 部门
    /// </summary>
    public class SystemDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Desc:编号
        /// </summary>           
        [Required(ErrorMessage = "编号不能为空")]
        [MaxLength(20, ErrorMessage = "编号最大长度:20")]
        public string code { get; set; }

        /// <summary>
        /// Desc:名称
        /// </summary>           
        [Required(ErrorMessage = "名称不能为空")]
        [MaxLength(20, ErrorMessage = "名称最大长度:20")]
        public string name { get; set; }

        /// <summary>
        /// Desc:状态(1-启用，0-停用)
        /// </summary>           
        [Required(ErrorMessage = "状态不能为空")]
        [Range(minimum: 0, maximum: 1, ErrorMessage = "状态值为:0/1")]
        public int state { get; set; }

        /// <summary>
        /// Desc:备注
        /// </summary>           
        [MaxLength(20, ErrorMessage = "备注最大长度:50")]
        public string remark { get; set; }
    }
}
