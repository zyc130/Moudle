using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.System;

namespace WMS.Moudle.DataAccess.Serveice.System
{
    internal class RoleDataAccess : BaseDataAccess, IRoleDataAccess
    {
        public RoleDataAccess(ISqlSugarClient client) : base(client) { }
    }
}
