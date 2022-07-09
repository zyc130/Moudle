using Microsoft.OpenApi.Models;
using WMS.Moudle.Api.Custom;
using WMS.Moudle.Api.Custom.Filter;
using WMS.Moudle.Utility;

var builder = WebApplication.CreateBuilder(args);

//加载配置文件
builder.Configuration.Bind("CustomConfig", StaticParams.CUSTOMCONFIG);

/// <summary>
/// 注入
/// </summary>
builder.Register();

// Add services to the container.
builder.Services.AddCors(CorsOptions =>
{
    CorsOptions.AddPolicy("CORS", PolicyBuilder =>
    {
        PolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


//注释
builder.Services.AddSwaggerGen(a =>
{
    //添加安全定义
    a.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //添加安全要求
    a.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{
                Reference =new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },new string[]{ }
        }
    });

    a.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WMS接口文档",
        Version = "v1",
        Description = "米其林WMS接口说明文档"
    });
    var apiPath = Path.Combine(AppContext.BaseDirectory, "WMS.Moudle.Api.xml");       //xml文档绝对路径
    var entityPath = Path.Combine(AppContext.BaseDirectory, "WMS.Moudle.Entity.xml"); //实体xml文档绝对路径
    a.IncludeXmlComments(apiPath, true);                                                //是否显示注释
    a.IncludeXmlComments(entityPath, true);                                             //是否显示注释
    a.OrderActionsBy(o => o.RelativePath);                                              //action名称排序
    a.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

app.UseCors("CORS");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//授权
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
