using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_menu")]
    public partial class sys_menu
    {
           public sys_menu(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:上一级id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? parent_id {get;set;}

           /// <summary>
           /// Desc:菜单名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string name {get;set;}

           /// <summary>
           /// Desc:地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string url {get;set;}

           /// <summary>
           /// Desc:icon地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string img {get;set;}

           /// <summary>
           /// Desc:操作按钮标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string action_key {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? order_no {get;set;}

           /// <summary>
           /// Desc:菜单级别
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int menu_level {get;set;}

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
