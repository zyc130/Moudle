using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///任务明细表
    ///</summary>
    [SugarTable("task_detial")]
    public partial class task_detial
    {
           public task_detial(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:任务id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long task_id {get;set;}

           /// <summary>
           /// Desc:模具类型编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string piece_code {get;set;}

           /// <summary>
           /// Desc:模具编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string fabrication_no {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string etat_code {get;set;}

           /// <summary>
           /// Desc:有效期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? due_date {get;set;}

           /// <summary>
           /// Desc:数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? number {get;set;}

           /// <summary>
           /// Desc:创建人id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long create_id {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:修改人id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long update_id {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime update_time {get;set;}

           /// <summary>
           /// Desc:状态(1-启用，0-停用)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int state {get;set;}

    }
}
