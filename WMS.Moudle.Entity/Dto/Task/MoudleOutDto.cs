using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Task
{
    /// <summary>
    /// 模具出库入参
    /// </summary>
    public class MoudleOutDto
    {
        /// <summary>
        /// 库存id
        /// </summary>
        [Required(ErrorMessage = "库存id不能为空")]
        public long id { get; set; }

        /// <summary>
        /// 是否部分出库(Yes-是,else-否)
        /// </summary>
        public bool? is_part { get; set; }

        /// <summary>
        /// 是否指定出库(true-是，else-否)
        /// </summary>
        public bool? is_special_out { get; set; }
    }
}
