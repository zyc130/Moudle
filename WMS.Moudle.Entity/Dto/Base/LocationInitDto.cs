using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Base
{
    /// <summary>
    /// 货位初始化dto
    /// </summary>
    public class LocationInitDto
    {
        /// <summary>
        /// 行号
        /// </summary>
        [Required(ErrorMessage = "行号不能为空")]
        [Range(minimum:1,maximum:4,ErrorMessage ="行号范围值：1~4")]
        public int RowIndex { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        [Required(ErrorMessage = "列数不能为空")]
        [Range(minimum: 1, maximum: 99, ErrorMessage = "列数范围值：1~99")]
        public int ColumnCount { get; set; }

        /// <summary>
        /// 层数
        /// </summary>
        [Required(ErrorMessage = "层数不能为空")]
        [Range(minimum: 1, maximum: 99, ErrorMessage = "层数范围值：1~99")]
        public int FloorCount { get; set; }
    }
}
