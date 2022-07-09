using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Enum
{
    /// <summary>
    /// 枚举
    /// </summary>
    public class CommonEnum
    {
        /// <summary>
        /// 状态
        /// </summary>
        public enum EState
        {
            /// <summary>
            /// 启用
            /// </summary>
            [Description("启用")]
            Use =1,

            /// <summary>
            /// 停用
            /// </summary>
            [Description("停用")]
            Stop =0
        }
    }
}
