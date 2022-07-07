using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Entity.Dto.Base
{
    /// <summary>
    /// 货位展示
    /// </summary>
    public class LocationShowDto
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNo { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 集合
        /// </summary>
        public List<LocationDto> DataList { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class LocationDto
    {
        /// <summary>
        /// Desc:巷到编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int roadway_no { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string location_code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string location_name { get; set; }

        /// <summary>
        /// Desc:货位类型0-默认，1-
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int location_type { get; set; }
    }
}
