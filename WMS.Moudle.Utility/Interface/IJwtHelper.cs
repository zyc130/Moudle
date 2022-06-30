using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static WMS.Moudle.Utility.StaticParams;

namespace WMS.Moudle.Utility.Interface
{
    /// <summary>
    /// JWT 工具类
    /// </summary>
    public interface IJwtHelper
    {
        /// <summary>
        /// 获取JWT token
        /// </summary>
        /// <param name="jWTOption"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        string GetJwtToken(JWTOption jWTOption, Claim[] claims);
    }
}
