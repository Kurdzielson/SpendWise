using SpendWise.Shared.Infrastructure.Postgres;

namespace SpendWise.Modules.Users.Core.Users.DAL;

internal class UsersUnitOfWork(UsersWriteDbContext dbContext) : PostgresUnitOfWork<UsersWriteDbContext>(dbContext);