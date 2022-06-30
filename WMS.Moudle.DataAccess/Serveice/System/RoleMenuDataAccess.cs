using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.System;

namespace WMS.Moudle.DataAccess.Serveice.System
{
    /// <summary>
    /// 权限
    /// </summary>
    internal class RoleMenuDataAccess : BaseDataAccess, IRoleMenuDataAccess
    {
        public RoleMenuDataAccess(ISqlSugarClient client) : base(client) { }
    }
}
