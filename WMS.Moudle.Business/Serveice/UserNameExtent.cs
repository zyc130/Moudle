using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Serveice.System;
using WMS.Moudle.Entity.Dto;
using WMS.Moudle.Entity.Models;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Business.Serveice
{
    public static class UserNameExtent
    {
        public static void SetName<T>(this T t,List<sys_user> users) where T : BaseListDto, new()
        {
            var properties = t.GetType().GetProperties();
            foreach (var p in properties)
            {
                if ("create_id".Equals(p.Name, StringComparison.OrdinalIgnoreCase))
                {
                    t.create_name = users?.FirstOrDefault(a => a.id == ConvertHelper.ToLong(p.GetValue(t)))?.real_name;
                }
                else if ("update_id".Equals(p.Name, StringComparison.OrdinalIgnoreCase))
                {
                    t.update_name = users?.FirstOrDefault(a => a.id == ConvertHelper.ToLong(p.GetValue(t)))?.real_name;
                }
            }
        }

        public static void SetName<T>(this List<T> ts, List<sys_user> users) where T : BaseListDto, new()
        {
            ts?.ForEach(a =>
            {
                a.SetName(users);
            });
        }
    }
}
