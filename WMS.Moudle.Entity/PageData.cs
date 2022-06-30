using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity
{
    /// <summary>
    /// 分页数据对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        
        /// <summary>
        /// 页码大小
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> DataList { get; set; }
    }
}
