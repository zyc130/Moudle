using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.DataAccess.Interface.Task;

namespace WMS.Moudle.DataAccess.Serveice.Task
{
    internal class TaskDetailDataAccess : BaseDataAccess, ITaskDetailDataAccess
    {
        public TaskDetailDataAccess(ISqlSugarClient client) : base(client)
        {
        }
    }
}
