using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.Entity.Dto.Task
{
    /// <summary>
    /// 部分回库
    /// </summary>
    public class TaskPartDto
    {
        /// <summary>
        /// Desc:任务号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string task_no { get; set; }

        /// <summary>
        /// Desc:托盘条码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string pallet_barcode { get; set; }

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
        /// Desc:任务类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        [DictionaryTag(EDicCode.task_type, "task_type")]
        public int task_type_name { get; set; }

        /// <summary>
        /// 部件列表
        /// </summary>
        public List<string> details { get; set; }
    }
}
