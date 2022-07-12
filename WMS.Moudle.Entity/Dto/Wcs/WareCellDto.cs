using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Wcs
{
    /// <summary>
    /// 获取货位
    /// </summary>
    public class WareCellDto
    {
        /// <summary>
        /// wms任务号
        /// </summary>
        [Required(ErrorMessage = "wms任务号不能为空")]
        public string WMSTaskNum { get; set; }

        /// <summary>
        /// 巷道号
        /// </summary>
        [Required(ErrorMessage = "巷道号不能为空")]
        public string TunnelNum { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string PickUpEquipmentNo { get; set; }

        /// <summary>
        /// Memo1
        /// </summary>
        public string? Memo1 { get; set; }

        /// <summary>
        /// Memo2
        /// </summary>
        public string? Memo2 { get; set; }
    }
}
