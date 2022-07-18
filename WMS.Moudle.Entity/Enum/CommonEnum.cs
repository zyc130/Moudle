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

            /// <summary>
            /// 货位状态
            /// </summary>
            location_state,

            /// <summary>
            /// 任务状态
            /// </summary>
            task_state,
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
            [Description("入库不检测")]
            Yes =1,

            /// <summary>
            /// 默认
            /// </summary>
            [Description("默认")]
            No =0,
        }

        /// <summary>
        /// 货架类型
        /// </summary>
        public enum ELocationType
        {
            /// <summary>
            /// 常规
            /// </summary>
            [Description("常规")]
            Common =0,

            /// <summary>
            /// 大货架
            /// </summary>
            [Description("大货架")]
            Big =1
        }

        /// <summary>
        /// 货架类型
        /// </summary>
        public enum ELocationState
        {
            /// <summary>
            /// 空货位
            /// </summary>
            [Description("空货位")]
            Empty = 1,

            /// <summary>
            /// 有货
            /// </summary>
            [Description("有货")]
            Use = 2,

            /// <summary>
            /// 禁用
            /// </summary>
            [Description("禁用")]
            Stop = 3,

            /// <summary>
            /// 入库中
            /// </summary>
            [Description("入库中")]
            InStock = 4,

            /// <summary>
            /// 出库中
            /// </summary>
            [Description("出库中")]
            OutStock = 5,

            /// <summary>
            /// 禁止出库
            /// </summary>
            [Description("禁止出库")]
            OutStockStop = 6,
        }

        /// <summary>
        /// 巷道
        /// </summary>
        public enum ERoadWay
        {
            /// <summary>
            /// 巷道1
            /// </summary>
            [Description("巷道1")]
            One =1,

            /// <summary>
            /// 巷道2
            /// </summary>
            [Description("巷道2")]
            Two =2
        }

        /// <summary>
        /// 是否部分出入库
        /// </summary>
        public enum EIsPart
        {
            /// <summary>
            /// 是
            /// </summary>
            [Description("是")]
            Yes = 1,

            /// <summary>
            /// 否
            /// </summary>
            [Description("否")]
            No = 0
        }
    }
}
