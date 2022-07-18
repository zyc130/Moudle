using System.Reflection;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api
{
    /// <summary>
    /// 特性扩展类型
    /// </summary>
    public static class AttributeHelper
    {

        /// <summary>
        /// 获取标记属性SummaryAttribute对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DictionaryTagAttribute GetDictionaryTagAttribute<T>(this T t) where T : PropertyInfo
        {
            if (t.IsDefined(typeof(DictionaryTagAttribute), true))
            {
                return (DictionaryTagAttribute)t.GetCustomAttribute(typeof(DictionaryTagAttribute));
            }
            return null;
        }

        /// <summary>
        /// 获取字典标注名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ToDictionaryName<T>(this T t, IDictionaryDetailBusiness dictionaryDetailBusiness) where T : class,new()
        {
            if (t==null)
            {
                return null;
            }
            var properties = t.GetType().GetProperties();
            foreach (var p in properties)
            {
                var attribute = p.GetDictionaryTagAttribute();
                if (attribute==null)
                {
                    continue;
                }
                var items = dictionaryDetailBusiness.FindByCode(attribute.DicCode.ToString());
                if (items?.Count == 0)
                {
                    continue;
                }
                var value = properties.FirstOrDefault(pValue => pValue.Name == attribute.ValuePropertyName)?.GetValue(t);
                var dicName = items?
                    .FirstOrDefault(a => a.dic_value == ConvertHelper.ToInt(value))?.dic_name;
                p.SetValue(t, dicName??string.Empty);
            }
            return t;
        }

        /// <summary>
        /// 获取字典标注名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> ToDictionaryName<T>(this List<T> ts, IDictionaryDetailBusiness dictionaryDetailBusiness) where T : class, new()
        {
            ts?.ForEach(t =>
            {
                t.ToDictionaryName(dictionaryDetailBusiness);
            });
            return ts;
        }
    }
}
