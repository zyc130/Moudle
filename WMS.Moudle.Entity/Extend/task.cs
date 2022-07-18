using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Dto;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.Entity.Models
{
    public partial class task : BaseListDto
    {
        /// <summary>
        /// Desc:是否入库
        /// </summary>           
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.is_in_stock, "is_in_stock")]
        public string is_in_stock_name { get; set; } = string.Empty;

        /// <summary>
        /// 任务类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.task_type, "task_type")]
        public string task_type_name { get; set; } = string.Empty;

        /// <summary>
        /// 任务状态
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.task_state, "task_state")]
        public string task_state_name { get; set; } = string.Empty;

        /// <summary>
        /// Desc:物件类型
        /// </summary>           
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.material_type, "material_type")]
        public string material_type_name { get; set; } = string.Empty;

        /// <summary>
        /// 状态name
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.sys_state, "state")]

        public string state_name { get; set; } = string.Empty;
    }
}
