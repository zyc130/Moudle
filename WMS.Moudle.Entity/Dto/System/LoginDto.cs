using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Entity.Dto.System
{
    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
