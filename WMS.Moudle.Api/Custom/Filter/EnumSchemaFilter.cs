using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using WMS.Moudle.Utility.Extend;

namespace WMS.Moudle.Api.Custom.Filter
{
    /// <summary>
    /// 枚举描述
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                StringBuilder stringBuilder = new();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(name =>
                    {
                        Enum e = (Enum)Enum.Parse(context.Type, name);
                        var data = $"{name}({e.ToDescription()})={Convert.ToInt64(Enum.Parse(context.Type, name))}";

                        stringBuilder.AppendLine(data);
                    });
                schema.Description = stringBuilder.ToString();

                schema.Type = context.Type.Name;
                schema.Format = context.Type.Name;
            }
        }
    }
}
