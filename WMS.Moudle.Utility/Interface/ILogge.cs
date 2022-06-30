using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Moudle.Utility.Interface
{
    /// <summary>
    /// 日志接口
    /// </summary>
    /// <typeparam name="T">写入对象</typeparam>
    public interface ILogge<T>
    {
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg">内容</param>
        /// <param name="path">自定义路径(默认为空)</param>
        void Debug(string msg, string path = "");

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg">内容</param>
        /// <param name="path">自定义路径(默认为空)</param>
        void Info(string msg, string path = "");

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="msg">内容</param>
        /// <param name="path">自定义路径(默认为空)</param>
        void Warning(string msg, string path = "");

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg">内容</param>
        /// <param name="path">自定义路径(默认为空)</param>
        void Error(string msg, string path = "");
    }
}
