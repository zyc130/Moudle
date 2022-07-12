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
    /// 模具入库入参
    /// </summary>
    public class MoudleInDto: MoudleCountDto
    {
        /// <summary>
        /// 托盘条码
        /// </summary>
        [MaxLength(100,ErrorMessage = "托盘条码最大长度为:100个字符")]
        public string? pallet_barcode { get; set; }

        /// <summary>
        /// 部件钢印集合
        /// </summary>
        [MaxLength(80,ErrorMessage = "部件钢印个数最多:80条")]
        public List<string>? codes { get; set; }
    }
}
