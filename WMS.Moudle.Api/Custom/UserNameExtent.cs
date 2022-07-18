using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Serveice.System;
using WMS.Moudle.Entity.Dto;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserNameExtent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="users"></param>
        public static void SetName<T>(this T t,List<sys_user> users) where T : BaseListDto, new()
        {
            if (users?.Count==0 || t==null)
            {
                return;
            }
            var properties = t.GetType().GetProperties();
            foreach (var p in properties)
            {
                if ("create_id".Equals(p.Name, StringComparison.OrdinalIgnoreCase))
                {
                    t.create_name = users?.FirstOrDefault(a => a.id == ConvertHelper.ToLong(p.GetValue(t)??0))?.real_name??string.Empty;
                }
                else if ("update_id".Equals(p.Name, StringComparison.OrdinalIgnoreCase))
                {
                    t.update_name = users?.FirstOrDefault(a => a.id == ConvertHelper.ToLong(p.GetValue(t)??0))?.real_name ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="users"></param>
        public static List<T> SetName<T>(this List<T> ts, List<sys_user> users) where T : BaseListDto, new()
        {
            ts?.ForEach(a =>
            {
                a.SetName(users);
            });
            return ts;
        }
    }
}
