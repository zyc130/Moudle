using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///任务表
    ///</summary>
    [SugarTable("task")]
    public partial class task
    {
           public task(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:任务号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string task_no {get;set;}

           /// <summary>
           /// Desc:是否入库(1-入库，2-出库)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int is_in_stock {get;set;}

           /// <summary>
           /// Desc:任务类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int task_type {get;set;}

           /// <summary>
           /// Desc:任务状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int task_state {get;set;}

           /// <summary>
           /// Desc:物件类型(模具托盘、整模、备件等)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? material_type {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string pallet_barcode {get;set;}

           /// <summary>
           /// Desc:数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int number {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string wcs_task_id {get;set;}

           /// <summary>
           /// Desc:巷到编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? roadway_no {get;set;}

           /// <summary>
           /// Desc:起始地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string start_point {get;set;}

           /// <summary>
           /// Desc:当前地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string now_point {get;set;}

           /// <summary>
           /// Desc:目标地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string end_point {get;set;}

           /// <summary>
           /// Desc:排序号(优先执行数值大的)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? sort_no {get;set;}

           /// <summary>
           /// Desc:状态(1-启用，0-停用)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int state {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:创建人id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long create_id {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime update_time {get;set;}

           /// <summary>
           /// Desc:修改人id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long update_id {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string remark {get;set;}

    }
}
