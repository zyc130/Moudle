using WMS.Moudle.Utility.Interface;
using log4net;
using log4net.Appender;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WMS.Moudle.Utility.Serveice
{
    internal class Logger<T> : ILogge<T>
    {
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="path"></param>
        public void Debug(string msg, string path = "")
        {
            Write(LogLevel.Debug, msg, path);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="path"></param>
        public void Error(string msg, string path = "")
        {
            Write(LogLevel.Error, msg, path);
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="path"></param>
        public void Info(string msg, string path = "")
        {
            Write(LogLevel.Information, msg, path);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="path"></param>
        public void Warning(string msg, string path = "")
        {
            Write(LogLevel.Warning, msg, path);
        }

        #region private

        private static ILog loger = null;
        private static FileAppender fileApd = null;
        private static string basePath = string.Empty;
        private static ILoggerRepository repository;
        static Logger()
        {
            if (repository==null)
            {
                repository = LogManager.CreateRepository(Guid.NewGuid().ToString());
            }
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\log4net.config")));
            fileApd = repository.GetAppenders().First() as FileAppender;
            basePath = fileApd.File;
            loger = LogManager.GetLogger(repository.Name, string.Empty);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param> 
        /// <param name="path"></param>
        private void Write(LogLevel level, string msg, string path)
        {
            string logFilePath = DateTime.Now.ToString("yyyyMMdd") + "\\{0}" + $"\\{DateTime.Now.Hour}.txt";
            string writePath = basePath + string.Format(logFilePath, $"{level}{(string.IsNullOrWhiteSpace(path) ? "" : $"\\{path}")}");
            if (writePath != fileApd.File)
            {
                fileApd.File = writePath;
                fileApd.ActivateOptions();
            }
            msg = $"{typeof(T).Name} {msg}";
            switch (level)
            {
                case LogLevel.Debug:
                    loger.Debug($"{msg}");
                    break;
                case LogLevel.Information:
                    loger.Info($"{msg}");
                    break;
                case LogLevel.Warning:
                    loger.Warn($"{msg}");
                    break;
                case LogLevel.Error:
                    loger.Error($"{msg}");
                    break;
                default:
                    loger.Info($"{msg}");
                    break;
            }
        }

        #endregion
    }
}
