using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WMS.Moudle.Entity.Enum.TaskEnum;

namespace WMS.Moudle.Entity.Dto.Task
{
    /// <summary>
    /// 空托盘入库
    /// </summary>
    public class PalletInDto
    {
        /// <summary>
        /// 托盘类型(4/5)
        /// </summary>
        [Required(ErrorMessage = "托盘类型不能为空")]
        [Range(minimum: 4, maximum: 5, ErrorMessage = "托盘类型值为:4/5")]
        public EMaterialType material_type { get; set; }
    }
}
