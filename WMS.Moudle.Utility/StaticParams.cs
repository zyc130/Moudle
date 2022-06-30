using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Moudle.Utility
{
    /// <summary>
    /// 静态参数
    /// </summary>
    public class StaticParams
    {
        public static CustomConfig CUSTOMCONFIG { get; set; }=new CustomConfig();
    }

    #region 配置信息

    /// <summary>
    /// 配置信息实体类
    /// </summary>
    public class CustomConfig
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        public JWTOption JwtOption { get; set; }

        public RedisConfig RedisConfig { get; set; }

        public List<RegisterKeyValue> RegisterMap { get; set; }
    }

    public class RegisterKeyValue
    {
        public string Interface { get; set; }

        public string Server { get; set; }
    }

    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// ip+port
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// db
        /// </summary>
        public int DefaultDatabase { get; set; }

        public override string ToString()
        {
            return $"{ServiceName},{nameof(Password)}={Password},{nameof(DefaultDatabase)}={DefaultDatabase}";
        }
    }

    /// <summary>
    /// JWT配置
    /// </summary>
    public class JWTOption
    {
        /// <summary>
        /// 使用者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 加密key
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 授权过期时间长
        /// </summary>
        public int ExpiresMinutes { get; set; }
    }

    #endregion

}
