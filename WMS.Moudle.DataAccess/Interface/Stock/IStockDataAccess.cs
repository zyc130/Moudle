using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.DataAccess.Interface.Stock
{
    public interface IStockDataAccess: IBaseDataAccess
    {
        int GetLocationEmptyCount(ELocationType type);
    }
}
