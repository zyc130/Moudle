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
    /// 获取模具数量
    /// </summary>
    public class MoudleCountDto
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        //[Required(ErrorMessage = "任务类型不能为空")]
        [Range(minimum: 1, maximum: 3, ErrorMessage = "任务类型值为:1~3")]
        public ETaskType task_type { get; set; }

        /// <summary>
        /// 物件类型
        /// </summary>
        [Range(minimum: 1, maximum: 3, ErrorMessage = "物件类型值为:1~3")]
        public EMaterialType? material_type { get; set; }
    }
}
