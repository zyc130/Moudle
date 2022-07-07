using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.Base;

namespace WMS.Moudle.DataAccess.Serveice.Base
{
    internal class LocationDataAccess : BaseDataAccess, ILocationDataAccess
    {
        public LocationDataAccess(ISqlSugarClient client) : base(client)
        {
        }
    }
}
