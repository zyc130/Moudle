using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.DataAccess.Interface.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Serveice.System
{
    internal class ConfigBusiness : IConfigBusiness
    {
        IConfigDataAccess configDataAccess;
        public ConfigBusiness(IConfigDataAccess _configDataAccess)
        {
            configDataAccess = _configDataAccess;
        }

        public (bool, string, sys_config) Add(sys_config t)
        {
            t.create_time = DateTime.Now;
            t.update_time = DateTime.Now;
            return (true, "", configDataAccess.Insert(t));
        }

        public bool Delete(long id)
        {
            return configDataAccess.Delete<sys_config>(id);
        }

        public sys_config Find(long id)
        {
            return configDataAccess.Find<sys_config>(id);
        }

        public List<sys_config> FindAll()
        {
            return configDataAccess.FindAll<sys_config>();
        }

        public (bool, string) Update(sys_config t)
        {
            if (!configDataAccess.UpdateColumns(t, a => new { a.value, a.remark }, w => w.id == t.id))
            {
                return (false, "修改失败!");
            }
            return (true, string.Empty);
        }
    }
}
