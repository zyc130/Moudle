using static WMS.Moudle.Entity.Enum.TaskEnum;

namespace WMS.Moudle.Entity.Dto.Task
{
    /// <summary>
    /// 任务分页入参
    /// </summary>
    public class TaskPageDto: BasePageDto
    { 
        /// <summary>
      /// 出入库类型
      /// </summary>
        public EIsInStock? is_in_stock { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public ETaskType? task_type { get; set; }

        /// <summary>
        /// 编号(目标地址)
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// 任务号
        /// </summary>
        public string? task_no { get; set; }
    }
}
