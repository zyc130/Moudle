using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.DataAccess.Interface.Base
{
    public interface IMaterialDataAccess : IBaseDataAccess
    {
        int Count();
    }
}
