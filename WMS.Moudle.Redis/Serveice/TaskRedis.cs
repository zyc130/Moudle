using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Redis.Interface;

namespace WMS.Moudle.Redis.Serveice
{
    internal class TaskRedis : ITaskRedis
    {
        #region 任务号

        private string GetTaskIncrKey()
        {
            return string.Format(RedisKey.Task_Incr, DateTime.Now.Day);
        }

        /// <summary>
        /// 获取任务号当天编号
        /// </summary>
        /// <returns></returns>
        public long GetTaskIndex(long? start)
        {
            string key = GetTaskIncrKey();
            if (start!=null && start>0)
            {
                RedisHelper.Set(key, start, BaseHandle.ExpTwoDay);
            }
            long index = RedisHelper.IncrBy(key);
            RedisHelper.Expire(key, BaseHandle.ExpTwoDay);
            return index;
        }
        #endregion
    }
}
