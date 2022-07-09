using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Task
{
    /// <summary>
    /// 空托盘出库
    /// </summary>
    public class PalletOutDto:PalletInDto
    {
        /// <summary>
        /// 出库数量
        /// </summary>
        [Required(ErrorMessage = "出库数量不能为空")]
        [Range(minimum: 1, maximum: 10, ErrorMessage = "出库数量值为:1~10")]
        public int number { get; set; }
    }
}
