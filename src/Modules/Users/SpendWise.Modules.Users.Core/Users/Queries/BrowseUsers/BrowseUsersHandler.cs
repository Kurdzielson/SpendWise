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
        var users = context.Users.AsNoTracking();
        users = Filter(query, users);

        var result = await users
            .Select(q => q.AsDto())
            .PaginateAsync(query, cancellationToken);

        return result;
    }

    private IQueryable<UserReadModel> Filter(BrowseUsersQuery query, IQueryable<UserReadModel> users)
    {
        if (!string.IsNullOrEmpty(query.Role))
            users = users.Where(q => q.Role.Name == query.Role.ToLowerInvariant());

        if (query.CreatedAtFrom is not null)
            users = users.Where(q => q.CreatedAt >= query.CreatedAtFrom);

        if (query.CreatedAtTo is not null)
            users = users.Where(q => q.CreatedAt <= query.CreatedAtTo);

        if (!string.IsNullOrEmpty(query.State))
            users = users.Where(q => q.State == query.State.ToLowerInvariant());

        users = query.SortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase)
            ? users.OrderByDescending(q => q.CreatedAt)
            : users.OrderBy(q => q.CreatedAt);

        return users;
    }
}