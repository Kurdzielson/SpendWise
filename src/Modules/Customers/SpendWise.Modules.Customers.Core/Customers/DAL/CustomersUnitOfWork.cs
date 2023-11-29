using SpendWise.Shared.Infrastructure.Postgres;

namespace SpendWise.Modules.Customers.Core.Customers.DAL;

internal class CustomersUnitOfWork(CustomersWriteDbContext dbContext)
    : PostgresUnitOfWork<CustomersWriteDbContext>(dbContext);