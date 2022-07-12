using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Task
{
    /// <summary>
    /// 任务明细
    /// </summary>
    public class TaskDetailDto
    {
        /// <summary>
        /// Desc:模具类型编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string piece_code { get; set; }

        /// <summary>
        /// Desc:模具编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string fabrication_no { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string etat_code { get; set; }

        /// <summary>
        /// Desc:有效期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? due_date { get; set; }
    }
}
