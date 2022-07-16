using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.Reflection;
using System.Text;
using WMS.Moudle.Api.Custom.Attribution;
using WMS.Moudle.Utility;

namespace WMS.Moudle.Api.Custom
{
    /// <summary>
    /// 注入扩展
    /// </summary>
    public static class BuilderExtend
    {
        /// <summary>
        /// 自定义内容
        /// </summary>
        /// <param name="builder"></param>
        public static void Register(this WebApplicationBuilder builder)
        {
            var config = StaticParams.CUSTOMCONFIG;

            //替换容器使用Autofac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(configureDelegate =>
            {

                //接口和实现注册
                config.RegisterMap?.ForEach(map =>
                {
                    //Assembly interfaceAssembly = Assembly.Load(map.Interface);
                    //Assembly serverAssembly = Assembly.Load(map.Server);
                    //configureDelegate.RegisterAssemblyTypes(interfaceAssembly, serverAssembly).AsImplementedInterfaces();

                    Assembly interfaces = Assembly.Load(map.Interface);
                    Assembly servers = Assembly.Load(map.Server);
                    configureDelegate.RegisterAssemblyTypes(interfaces, servers).Where(a => !a.IsGenericType)
                    .AsImplementedInterfaces();
                    //.PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)

                    var InterfaceTypes = interfaces.GetTypes().Where(a => a.IsGenericType && a.IsInterface).ToList();

                    var serverTypes = servers.GetTypes().Where(a => a.IsClass && a.IsGenericType).ToList();
                    InterfaceTypes?.ForEach(i =>
                    {
                        var type = serverTypes.Find(c => c.GetInterface(i.Name) != null);
                        if (type != null)
                        {
                            configureDelegate.RegisterGeneric(type).As(i);
                            //.PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                        }
                    });
                });
                //sqlsugar
                configureDelegate.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient sqlSugarClient = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString = config.ConnectionString,
                        DbType = DbType.SqlServer,
                        InitKeyType = InitKeyType.Attribute,
                        IsAutoCloseConnection = true
                    });
                    return sqlSugarClient;
                }).InstancePerLifetimeScope();
                //redis
                RedisHelper.Initialization(new CSRedis.CSRedisClient(config.RedisConfig.ToString()));

            });

            //使用鉴权JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(option =>
                    {
                        option.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = config.JwtOption.Issuer,
                            ValidateAudience = true,
                            ValidAudience = config.JwtOption.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtOption.SecurityKey)),
                            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                            {
                                return notBefore <= DateTime.UtcNow && expires >= DateTime.UtcNow;
                            }
                        };
                    });

            builder.Services.AddControllers(c => c.Filters.Add(typeof(ApiActionFilterAttribute)));
        }
    }
}
