using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("SpendWise.Modules.Expenses.Api")]
[assembly: InternalsVisibleTo("SpendWise.Modules.Expenses.Infrastructure")]

namespace SpendWise.Modules.Expenses.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services;
}