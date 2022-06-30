using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///用户表
    ///</summary>
    [SugarTable("sys_user")]
    public partial class sys_user
    {
           public sys_user(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:用户名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string name {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string password {get;set;}

           /// <summary>
           /// Desc:编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string code {get;set;}

           /// <summary>
           /// Desc:姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string real_name {get;set;}

           /// <summary>
           /// Desc:部门id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? dept_id {get;set;}

           /// <summary>
           /// Desc:角色id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? role_id {get;set;}

           /// <summary>
           /// Desc:权限状态(0-默认，1-入库不检验)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int power_state {get;set;}

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
           /// Desc:备注说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string remark {get;set;}

    }
}
