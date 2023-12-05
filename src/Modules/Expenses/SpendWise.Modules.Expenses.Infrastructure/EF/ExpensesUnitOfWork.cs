using SpendWise.Shared.Infrastructure.Postgres;

namespace SpendWise.Modules.Expenses.Infrastructure.EF;

internal class ExpensesUnitOfWork(ExpensesWriteDbContext dbContext)
    : PostgresUnitOfWork<ExpensesWriteDbContext>(dbContext);