using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Entity.Dto.Stock
{
    /// <summary>
    /// 分页展示实体
    /// </summary>
    public class StockShowDto:stock
    {

        /// <summary>
        /// Desc:模具类型编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string piece_code { get; set; }=string.Empty;

        /// <summary>
        /// Desc:模具编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string fabrication_no { get; set; } = string.Empty;
    }
}
