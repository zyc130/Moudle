using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto
{
    /// <summary>
    /// 分页
    /// </summary>
    public class BasePageDto
    {
        /// <summary>
        /// 检索页码
        /// </summary>
        [Required(ErrorMessage = "页码不能为空")]
        public int pageIndex { get; set; } = 1;

        /// <summary>
        /// 页码大小
        /// </summary>
        [Required(ErrorMessage = "页码大小不能为空")]
        public int pageSize { get; set; } = 20;
    }
}
