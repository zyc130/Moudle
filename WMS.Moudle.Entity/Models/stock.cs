using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace WMS.Moudle.Entity.Models
{
    ///<summary>
    ///库存主表
    ///</summary>
    [SugarTable("stock")]
    public partial class stock
    {
           public stock(){


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
           /// Desc:货位编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string location_code {get;set;}

           /// <summary>
           /// Desc:货位状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int location_state {get;set;}

           /// <summary>
           /// Desc:托盘条码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string pallet_barcode {get;set;}

           /// <summary>
           /// Desc:任务id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long task_id {get;set;}

           /// <summary>
           /// Desc:任务类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int task_type {get;set;}

           /// <summary>
           /// Desc:是否入库(1-入库，2-出库)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int is_in_stock {get;set;}

           /// <summary>
           /// Desc:物件类型(1-模具托盘，2-模套托盘，3-模具)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int material_type {get;set;}

           /// <summary>
           /// Desc:状态(1-启用，0-默认)
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
