using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_role_menu")]
    public partial class sys_role_menu
    {
           public sys_role_menu(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:角色id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long role_id {get;set;}

           /// <summary>
           /// Desc:菜单id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long menu_id {get;set;}

    }
}
