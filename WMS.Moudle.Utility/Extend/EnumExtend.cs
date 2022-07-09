using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility.Extend
{
    public static class EnumExtend
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum e)
        {
            var description = e.ToString();

            var attrs = e.GetType().GetField(description)?.GetCustomAttributes(false);
            if (attrs?.Count()==0)
            {
                return description;
            }
            foreach (var attr in attrs)
            {
                if (attr is DescriptionAttribute)
                {
                    description = ((DescriptionAttribute)attr).Description;
                    break;
                }
            }
            return description;
        }
    }
}
