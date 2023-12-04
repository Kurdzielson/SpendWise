using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Expenses.Infrastructure.EF;
using SpendWise.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("SpendWIse.Modules.Expenses.Api")]

namespace SpendWise.Modules.Expenses.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
            .AddPostgres<ExpensesWriteDbContext>()
            .AddPostgres<ExpensesReadDbContext>();
}