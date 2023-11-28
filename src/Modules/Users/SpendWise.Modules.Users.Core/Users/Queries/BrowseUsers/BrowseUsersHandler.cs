using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Users.Core.Users.DAL;
using SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read.Model;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Shared.Abstraction.Queries;
using SpendWise.Shared.Infrastructure.Postgres;

namespace SpendWise.Modules.Users.Core.Users.Queries.BrowseUsers;

internal class BrowseUsersHandler(UsersReadDbContext context) : IQueryHandler<BrowseUsersQuery, Paged<UserDto>>
{
    public async Task<Paged<UserDto>> HandleAsync(BrowseUsersQuery query, CancellationToken cancellationToken = default)
    {
        var users = context.Users
            .AsNoTracking();
        users = Filter(query, users);

        var result = await users
            .Include(q => q.Role)
            .Select(q => q.AsDto())
            .PaginateAsync(query, cancellationToken);

        return result;
    }

    private IQueryable<UserReadModel> Filter(BrowseUsersQuery query, IQueryable<UserReadModel> users)
    {
        if (!string.IsNullOrEmpty(query.Role))
            users = users.Where(q => EF.Functions.Like(q.Role.Name, query.Role.ToLowerInvariant()));

        if (query.CreatedAtFrom is not null)
            users = users.Where(q => q.CreatedAt >= query.CreatedAtFrom);

        if (query.CreatedAtTo is not null)
            users = users.Where(q => q.CreatedAt <= query.CreatedAtTo);

        if (!string.IsNullOrEmpty(query.State))
            users = users.Where(q => EF.Functions.Like(q.State, query.State));

        users = !string.IsNullOrWhiteSpace(query.SortOrder) &&
                query.SortOrder.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
            ? users.OrderBy(q => q.CreatedAt)
            : users.OrderByDescending(q => q.CreatedAt);

        return users;
    }
}