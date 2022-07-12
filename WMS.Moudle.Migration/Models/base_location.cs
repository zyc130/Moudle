using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///货位表
    ///</summary>
    [SugarTable("base_location")]
    public partial class base_location
    {
           public base_location(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:巷到编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int roadway_no {get;set;}

           /// <summary>
           /// Desc:行编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int row_no {get;set;}

           /// <summary>
           /// Desc:列编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int column_no {get;set;}

           /// <summary>
           /// Desc:层编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int floor_no {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string location_code {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string location_name {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int location_type {get;set;}

           /// <summary>
           /// Desc:优先级排序
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
