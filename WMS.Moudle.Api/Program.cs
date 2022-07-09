using Microsoft.OpenApi.Models;
using WMS.Moudle.Api.Custom;
using WMS.Moudle.Api.Custom.Filter;
using WMS.Moudle.Utility;

var builder = WebApplication.CreateBuilder(args);

//���������ļ�
builder.Configuration.Bind("CustomConfig", StaticParams.CUSTOMCONFIG);

/// <summary>
/// ע��
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


//ע��
builder.Services.AddSwaggerGen(a =>
{
    //��Ӱ�ȫ����
    a.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "������token,��ʽΪ Bearer xxxxxxxx��ע���м�����пո�",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //��Ӱ�ȫҪ��
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
        Title = "WMS�ӿ��ĵ�",
        Version = "v1",
        Description = "������WMS�ӿ�˵���ĵ�"
    });
    var apiPath = Path.Combine(AppContext.BaseDirectory, "WMS.Moudle.Api.xml");       //xml�ĵ�����·��
    var entityPath = Path.Combine(AppContext.BaseDirectory, "WMS.Moudle.Entity.xml"); //ʵ��xml�ĵ�����·��
    a.IncludeXmlComments(apiPath, true);                                                //�Ƿ���ʾע��
    a.IncludeXmlComments(entityPath, true);                                             //�Ƿ���ʾע��
    a.OrderActionsBy(o => o.RelativePath);                                              //action��������
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

//��Ȩ
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
