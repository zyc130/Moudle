using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Base
{
    /// <summary>
    /// 分页入参
    /// </summary>
    public class MaterialPageDto : BasePageDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// 编码类型
        /// </summary>
        public string? code_type { get; set; }
    }
}
