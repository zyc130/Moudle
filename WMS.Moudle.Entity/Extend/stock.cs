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
    public partial class stock : BaseListDto
    {
        /// <summary>
        /// 货位状态
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.location_state,"location_state")]
        public string location_state_name { get; set; } = string.Empty;

        /// <summary>
        /// 任务类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.task_type, "task_type")]
        public string task_type_name { get; set; } = string.Empty;

        /// <summary>
        /// 是否出入库
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.is_in_stock, "is_in_stock")]
        public string is_in_stock_name { get; set; } = string.Empty;

        /// <summary>
        /// Desc:物件类型
        /// </summary>           
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.material_type, "material_type")]
        public string material_type_name { get; set; }

        /// <summary>
        /// Desc:状态名
        /// </summary>           
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.sys_state, "state")]
        public string state_name { get; set; }
    }
}
