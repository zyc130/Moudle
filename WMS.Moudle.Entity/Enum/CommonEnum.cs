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

        /// <summary>
        /// 字典类型
        /// </summary>
        public enum EDicCode
        {
            /// <summary>
            /// 状态
            /// </summary>
            sys_state,

            /// <summary>
            /// 物件类型
            /// </summary>
            material_type,

            /// <summary>
            /// 任务类型
            /// </summary>
            task_type,

            /// <summary>
            /// 出入库类型
            /// </summary>
            is_in_stock,
        }

        /// <summary>
        /// 配置编码
        /// </summary>
        public enum EConfigCode
        {
            /// <summary>
            /// 模具-部件入库扫码行数
            /// </summary>
            moudle_part,

            /// <summary>
            /// 模具-整模入库扫码行数
            /// </summary>
            moudle_whole,

            /// <summary>
            /// 模具-备件入库扫码行数
            /// </summary>
            moudle_spare,

            /// <summary>
            /// 是否扫描托盘
            /// </summary>
            is_scan_pallet
        }

        /// <summary>
        /// 特殊权限标识
        /// </summary>
        public enum EPowerState
        {
            /// <summary>
            /// 入库不检测
            /// </summary>
            Yes=1,
            
            /// <summary>
            /// 默认
            /// </summary>
            No=0,
        }

        /// <summary>
        /// 货架类型
        /// </summary>
        public enum ELocationType
        {
            /// <summary>
            /// 常规
            /// </summary>
            Common=0,

            /// <summary>
            /// 大货架
            /// </summary>
            Big=1
        }
    }
}
