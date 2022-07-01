using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility.Interface
{
    public interface IExcelHelper
    {
        /// <summary>
        /// 文件流转list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="steetIndex"></param>
        /// <returns></returns>
        List<T> ExcelToList<T>(Stream stream, int steetIndex = 0) where T : class;

        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        byte[] Export<T>(List<T> ts);
    }
}
