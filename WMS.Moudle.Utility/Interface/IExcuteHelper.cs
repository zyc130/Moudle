using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility.Interface
{
    /// <summary>
    /// 执行帮助类
    /// </summary>
    public interface IExcuteHelper
    {
        /// <summary>
        /// 异常抓取
        /// </summary>
        /// <param name="action"></param>
        void Try(Action action);

        /// <summary>
        /// 异常抓取返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        (bool,T) Func<T>(Func<(bool, T)> func);

        /// <summary>
        /// 执行SqlSugar分布式事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        (bool, T) Tran<T>(Func<(bool, T)> func);
    }
}
