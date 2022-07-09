using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility.Extend
{
    public static class ObjectSetTimeExtend
    {
        public static void SetNowTime<T>(this T t, List<string> propertyNames) where T : class, new()
        {
            var properties = t.GetType().GetProperties();
            foreach (var p in properties)
            {
                if (propertyNames.Exists(a=>a.Equals(p.Name)))
                {
                    if (p.PropertyType == typeof(DateTime))
                    {
                        p.SetValue(t, DateTime.Now);
                    }
                }
            }
        }

        public static void SetNowTime<T>(this List<T> ts, List<string> propertyNames) where T : class, new()
        {
            ts?.ForEach(t =>
            {
                var properties = t.GetType().GetProperties();
                foreach (var p in properties)
                {
                    if (propertyNames.Exists(a => a.Equals(p.Name)))
                    {
                        if (p.PropertyType == typeof(DateTime))
                        {
                            p.SetValue(t, DateTime.Now);
                        }
                    }
                }
            });
        }
    }
}
