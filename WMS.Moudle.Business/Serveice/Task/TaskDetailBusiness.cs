using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Base;
using WMS.Moudle.Business.Interface.Task;
using WMS.Moudle.DataAccess.Interface.Task;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.CommonEnum;

namespace WMS.Moudle.Business.Serveice.Task
{
    internal class TaskDetailBusiness : ITaskDetailBusiness
    {
        ITaskDetailDataAccess detailDataAccess;
        IMaterialBusiness materialBusiness;
        IMapper mapper;

        public TaskDetailBusiness(ITaskDetailDataAccess _detailDataAccess
            , IMaterialBusiness _materialBusiness
            , IMapper _mapper)
        {
            detailDataAccess = _detailDataAccess;
            materialBusiness = _materialBusiness;
            mapper = _mapper;
        }

        /// <summary>
        /// 批量插入任务明细
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int Insert(List<task_detail> ts)
        {
           return detailDataAccess.Insert(ts);
        }

        /// <summary>
        /// 格式化任务数据
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool, string, List<task_detail>) FormatDetails(List<string> codes,sys_user user)
        {
            var result = new Tuple<bool, string, List<task_detail>>(true, string.Empty, null);
            List<task_detail> items = new();
            codes?.ForEach(code => {
                if (!result.Item1)
                {
                    return;
                }
                var material = materialBusiness.Find(code);
                if (material == null)
                {
                    if (user.power_state == EPowerState.No.GetHashCode())
                    {
                        result = new(false, $"部件编号:{code},不存在!", items);
                        return;
                    }
                    material = new base_material()
                    {
                        fabrication_no = code
                    };
                }
                var dto = mapper.Map<TaskDetailDto>(material);
                var detail = mapper.Map<task_detail>(dto);
                detail.create_id = user.id;
                detail.update_id = user.id;
                detail.state = EState.Use.GetHashCode();
                detail.number = 1;
                items.Add(detail);
            });
            return (true,string.Empty, items);
        }

        /// <summary>
        /// 获取明细列表
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<task_detail> GetByTaskId(long taskId)
        {
            return detailDataAccess.Query<task_detail>(a => a.task_id == taskId)?.ToList();
        }
    }
}
