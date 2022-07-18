using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.Entity.Dto.Stock
{
    /// <summary>
    /// 库存分页入参
    /// </summary>
    public class StockPageDto:BasePageDto
    {
        /// <summary>
        /// 巷道号
        /// </summary>
        public ERoadWay? roadway_no { get; set; }

        /// <summary>
        /// 货位号
        /// </summary>
        public string? location_code { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public string? type { get; set; }

        /// <summary>
        /// 是否部分出库(true-是，else-否)
        /// </summary>
        public bool? is_part { get; set; }

        /// <summary>
        /// 是否指定出库(true-是，else-否)
        /// </summary>
        public bool? is_special_out { get; set; }
    }
}
