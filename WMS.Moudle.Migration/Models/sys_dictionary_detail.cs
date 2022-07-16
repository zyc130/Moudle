using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///字典键值表
    ///</summary>
    [SugarTable("sys_dictionary_detail")]
    public partial class sys_dictionary_detail
    {
           public sys_dictionary_detail(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:字典类型(主表)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string dic_code {get;set;}

           /// <summary>
           /// Desc:字典名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string dic_name {get;set;}

           /// <summary>
           /// Desc:字典键值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int dic_value {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int dic_sort {get;set;}

           /// <summary>
           /// Desc:状态(0-停用，1-启用)
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
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string remark {get;set;}

    }
}
