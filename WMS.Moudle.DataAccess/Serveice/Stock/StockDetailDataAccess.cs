using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.Stock;

namespace WMS.Moudle.DataAccess.Serveice.Stock
{
    internal class StockDetailDataAccess : BaseDataAccess, IStockDetailDataAccess
    {
        public StockDetailDataAccess(ISqlSugarClient client) : base(client)
        {
        }
    }
}
