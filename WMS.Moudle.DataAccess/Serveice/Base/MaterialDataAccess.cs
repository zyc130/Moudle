using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.Base;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.DataAccess.Serveice.Base
{
    internal class MaterialDataAccess : BaseDataAccess, IMaterialDataAccess
    {
        public MaterialDataAccess(ISqlSugarClient client) : base(client) { }

        public int Count()
        {
            return _client.Queryable<base_material>().Count();
        }
    }
}
