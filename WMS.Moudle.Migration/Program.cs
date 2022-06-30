// See https://aka.ms/new-console-template for more information
using SqlSugar;

Console.WriteLine("Hello, World!");

Console.WriteLine("=========sqlsugar 生成实体===========");

try
{

    ConnectionConfig connectionConfig = new ConnectionConfig()
    {
        DbType = DbType.SqlServer,
        ConnectionString = "Server=.; Database=WMS_Michelin_MJ;User=sa;Password=123; Trusted_Connection=True;Integrated Security=False;MultipleActiveResultSets=True;",
        InitKeyType = InitKeyType.Attribute,
        IsAutoCloseConnection = true,
    };

    using (ISqlSugarClient client = new SqlSugarClient(connectionConfig))
    {
        //一、基于数据库生成实体对象--DbFirst 
        {
            client.DbFirst
                .IsCreateAttribute()
                .CreateClassFile(@"D:\BOZONG\米其林模组\service\Moudle\WMS.Moudle.Migration\Models", "WMS.Moudle.Entity.Models");
        }
    }

}
catch (Exception ex)
{
    throw ex;
}
