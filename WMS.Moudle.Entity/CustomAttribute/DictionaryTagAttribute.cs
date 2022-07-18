using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.Entity
{
    /// <summary>
    /// 字典标识特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DictionaryTagAttribute : Attribute
    {
        public DictionaryTagAttribute(EDicCode dicCode,string valuePropertyName)
        {
            DicCode = dicCode;
            ValuePropertyName = valuePropertyName;
        }

        /// <summary>
        /// 字典类型
        /// </summary>
        public EDicCode DicCode { get; set; }

        /// <summary>
        /// 对应value属性名
        /// </summary>
        public string ValuePropertyName { get; set; }
    }
}
