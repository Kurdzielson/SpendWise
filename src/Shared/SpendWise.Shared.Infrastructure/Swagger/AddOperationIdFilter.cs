using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SpendWise.Shared.Infrastructure.Swagger;

internal abstract class AddOperationIdFilter : IOperationFilter
{
    private static readonly Regex MyRegex = new Regex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled);

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId != null) return;

        var controllerName = context.MethodInfo.DeclaringType?.Name;
        var actionName = context.MethodInfo.Name;

        operation.OperationId = GetOperationId(controllerName, actionName);
    }

    private static string GetOperationId(string className, string actionName)
        => MyRegex.Replace(className, "_$1").Trim() + "_" + MyRegex.Replace(actionName, "_$1").Trim();
}