using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 部门分页查询入参
    /// </summary>
    public class SystemPageDto:BasePageDto
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? state { get; set; }
    }
}
