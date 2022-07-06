using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 系统配置分页入参
    /// </summary>
    public class ConfigPageDto : BasePageDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? name { get; set; }
    }
}
