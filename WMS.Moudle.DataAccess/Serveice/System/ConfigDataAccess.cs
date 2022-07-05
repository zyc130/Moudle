using SqlSugar;
using WMS.Moudle.DataAccess.Interface.System;

namespace WMS.Moudle.DataAccess.Serveice.System
{
    internal class ConfigDataAccess : BaseDataAccess, IConfigDataAccess
    {
        public ConfigDataAccess(ISqlSugarClient client) : base(client)
        {
        }
    }
}
