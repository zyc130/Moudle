using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public class ConfigDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Desc:值(1-启用，0-停用)
        /// </summary>           
        [Required(ErrorMessage = "值不能为空")]
        [Range(minimum: 0, maximum: 99, ErrorMessage = "状态值为:0~99")]
        public int value { get; set; }

        /// <summary>
        /// Desc:备注
        /// </summary>           
        [MaxLength(50, ErrorMessage = "备注最大长度:50")]
        public string? remark { get; set; }
    }
}
