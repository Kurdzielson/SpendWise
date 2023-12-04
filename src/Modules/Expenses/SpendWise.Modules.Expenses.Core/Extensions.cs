using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("SpendWise.Modules.Expenses.Api")]
[assembly: InternalsVisibleTo("SpendWise.Modules.Expenses.Application")]
[assembly: InternalsVisibleTo("SpendWise.Modules.Expenses.Infrastructure")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SpendWise.Modules.Expenses.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services;
}