using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Stock
{
    /// <summary>
    /// 库存入参实体
    /// </summary>
    public class StockDto
    {
        /// <summary>
        /// Desc:巷到编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int roadway_no { get; set; }

        /// <summary>
        /// Desc:货位编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string location_code { get; set; }

        /// <summary>
        /// Desc:托盘条码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string pallet_barcode { get; set; }

        /// <summary>
        /// Desc:任务id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long task_id { get; set; }

        /// <summary>
        /// Desc:任务类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int task_type { get; set; }

        /// <summary>
        /// Desc:是否入库(1-入库，2-出库)
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int is_in_stock { get; set; }

        /// <summary>
        /// Desc:物件类型(1-模具托盘，2-模套托盘，3-模具)
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int material_type { get; set; }

        /// <summary>
        /// Desc:是否部分出入库(1-是，0-否)
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int is_part { get; set; }
    }
}
