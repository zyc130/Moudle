using Npoi.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Utility.Serveice
{
    internal class ExcelHelper: IExcelHelper
    {
        IExcuteHelper excuteHelper;
        public ExcelHelper(IExcuteHelper _excuteHelper)
        {
            excuteHelper= _excuteHelper;
        }

        public List<T> ExcelToList<T>(Stream stream,int steetIndex=0) where T:class
        {
          return  excuteHelper.Try<List<T>>(() =>
            {
                var mapper = new Mapper(stream);
                List<RowInfo<T>>? rowInfos = new();
                rowInfos = mapper.Take<T>(steetIndex)?.ToList();

                List<T> result = new();
                rowInfos?.ForEach(r =>
                {
                    result.Add(r.Value);
                });
                return result;
            });
        }

        /// <summary>
        /// 导出文件流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        public byte[] Export<T>(List<T> ts)
        {
            var mapper = new Mapper();
            using (MemoryStream stream = new())
            {
                mapper.Save(stream, ts, "sheet1", overwrite: true, xlsx: true);

                return stream.ToArray();
            } 
        }
    }
}
