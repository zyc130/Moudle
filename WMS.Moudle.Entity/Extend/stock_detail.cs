using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Models
{
    public partial class stock_detail
    {
        /// <summary>
        /// Desc:货位编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsIgnore = true)]
        public string location_code { get; set; }
    }
}
