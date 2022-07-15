using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto
{
    /// <summary>
    /// 列表
    /// </summary>
    public class BaseListDto
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string create_name { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string update_name { get; set; } = string.Empty;
    }
}
