using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Redis.Interface
{
    public interface ITaskRedis
    {
        long GetTaskIndex(long? start);
    }
}
