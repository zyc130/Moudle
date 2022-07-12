using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Redis
{
    /// <summary>
    /// RedisKey
    /// </summary>
    internal class RedisKey
    {
        #region task

        /// <summary>
        /// 每日自增key
        /// </summary>
        public const string Task_Incr = "task_incr:{0}";

        #endregion
    }
}
