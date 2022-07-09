using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Enum
{
    /// <summary>
    /// 任务枚举类
    /// </summary>
    public class TaskEnum
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public enum ETaskType
        {
            /// <summary>
            /// 模具入库
            /// </summary>
            [Description("模具入库")]
            MoudleIn =1,

            /// <summary>
            /// 模套入库
            /// </summary>
            [Description("模套入库")]
            MoudleSetIn = 2,

            /// <summary>
            /// 其它入库
            /// </summary>
            [Description("其它入库")]
            OtherIn = 3,

            /// <summary>
            /// 空托盘入库
            /// </summary>
            [Description("空托盘入库")]
            PalletEmptyIn =4,

            /// <summary>
            /// 模具出库
            /// </summary>
            [Description("模具出库")]
            MoudleOut = 11,

            /// <summary>
            /// 模套出库
            /// </summary>
            [Description("模套出库")]
            MoudleSetOut = 12,

            /// <summary>
            /// 其它出库
            /// </summary>
            [Description("其它出库")]
            OtherOut = 13,

            /// <summary>
            /// 空托盘出库
            /// </summary>
            [Description("空托盘出库")]
            PalletEmptyOut = 14,

            /// <summary>
            /// 指定出库
            /// </summary>
            [Description("指定出库")]
            SpecialOut = 15,
        }

        /// <summary>
        /// 物料类型
        /// </summary>
        public enum EMaterialType
        {
            /// <summary>
            /// 部件
            /// </summary>
            [Description("部件")]
            Part =1,

            /// <summary>
            /// 整模
            /// </summary>
            [Description("整模")]
            Whole =2,

            /// <summary>
            /// 备件
            /// </summary>
            [Description("备件")]
            Spare =3,

            /// <summary>
            /// 模具托盘
            /// </summary>
            [Description("模具托盘")]
            PalletMoudle =4,

            /// <summary>
            /// 模套托盘
            /// </summary>
            [Description("模套托盘")]
            PalletSet=5,
        }

        /// <summary>
        /// 出入库类型
        /// </summary>
        public enum EIsInStock
        {
            /// <summary>
            /// 入库
            /// </summary>
            [Description("入库")]
            Yes =1,

            /// <summary>
            /// 出库
            /// </summary>
            [Description("出库")]
            No =2
        }

        /// <summary>
        /// 任务状态
        /// </summary>
        public enum ETaskState
        {
            /// <summary>
            /// 排队中
            /// </summary>
            [Description("排队中")]
            Wait =0
        }
    }
}
