using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SpendWise.Shared.Infrastructure.Swagger;

internal class AddOperationIdFilter : IOperationFilter
{
    private static readonly Regex MyRegex = new Regex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled);

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId == null)
        {
            var controllerName = context.MethodInfo.DeclaringType?.Name;
            operation.OperationId = GetOperationId(controllerName);
        }
    }

    private static string GetOperationId(string className)
    {
        return MyRegex.Replace(className, "_$1").Trim();
    }
}