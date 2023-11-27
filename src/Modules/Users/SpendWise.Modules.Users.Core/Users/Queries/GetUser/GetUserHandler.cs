using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Users.Core.Users.DAL;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Users.Core.Users.Queries.GetUser;

internal class GetUserHandler(UsersReadDbContext dbContext) : IQueryHandler<GetUserQuery, UserDetailsDto?>
{
    public async Task<UserDetailsDto?> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Users
            .AsNoTracking()
            .Include(q => q.Role)
            .Where(q=>q.Id == query.UserId)
            .Select(q=> q.AsDetailsDto())
            .SingleOrDefaultAsync(cancellationToken);

        return result;
    }
}