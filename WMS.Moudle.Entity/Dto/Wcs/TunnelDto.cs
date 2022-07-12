using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.Wcs
{
    /// <summary>
    /// 获取巷道入参
    /// </summary>
    public class TunnelDto
    {
        /// <summary>
        /// wms任务号
        /// </summary>
        [Required(ErrorMessage = "wms任务号不能为空")]
        public string WMSTaskNum { get; set; }

        /// <summary>
        /// 巷到列表
        /// </summary>
        //[Required(ErrorMessage = "巷到列表不能为空")]
        public List<Dictionary<string,int>> SrmCurTunnelList { get; set; }

        /// <summary>
        /// Memo1
        /// </summary>
        public string? Memo1 { get; set; }

        /// <summary>
        /// Memo2
        /// </summary>
        public string? Memo2 { get; set; }

    }


}
