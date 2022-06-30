using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///角色表
    ///</summary>
    [SugarTable("sys_role")]
    public partial class sys_role
    {
           public sys_role(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:角色编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string code {get;set;}

           /// <summary>
           /// Desc:角色名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string name {get;set;}

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
