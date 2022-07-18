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
    public partial class sys_user:BaseListDto
    {
        /// <summary>
        /// 状态name
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [DictionaryTag(EDicCode.sys_state, "state")]

        public string state_name { get; set; } = string.Empty;
    }
}
