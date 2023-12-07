using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Expenses.Core.Expenses.Repositories;
using SpendWise.Modules.Expenses.Infrastructure.EF;
using SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Repositories;
using SpendWise.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("SpendWIse.Modules.Expenses.Api")]

namespace SpendWise.Modules.Expenses.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddUnitOfWork<ExpensesUnitOfWork>()
            .AddPostgres<ExpensesWriteDbContext>()
            .AddPostgres<ExpensesReadDbContext>();
}