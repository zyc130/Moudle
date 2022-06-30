using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Utility.Serveice
{
    internal class ExcuteHelper : IExcuteHelper
    {
        ILogge<ExcuteHelper> logge;
        protected ISqlSugarClient client;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_logge"></param>
        public ExcuteHelper(ILogge<ExcuteHelper> _logge, ISqlSugarClient _client)
        {
            logge = _logge;
            client = _client;
        }

        /// <summary>
        /// 执行带参函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public (bool, T) Func<T>(Func<(bool, T)> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                logge.Error($"msg:{ex.Message},StackTrace:{ex.StackTrace}");
                return new(false, default);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool, T) Tran<T>(Func<(bool, T)> func)
        {
            client.Ado.BeginTran();
            var exec = Func<T>(func);
            if (exec.Item1)
            {
                client.Ado.CommitTran();
            }
            else
            {
                client.Ado.RollbackTran();
            }
            return exec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Try(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                logge.Error($"msg:{ex.Message},StackTrace:{ex.StackTrace}");
            }
        }
    }
}
