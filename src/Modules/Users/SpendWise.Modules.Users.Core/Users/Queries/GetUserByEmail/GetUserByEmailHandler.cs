using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Users.Core.Users.DAL;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Users.Core.Users.Queries.GetUserByEmail;

internal class GetUserByEmailHandler(UsersReadDbContext dbContext) : IQueryHandler<GetUserByEmailQuery, UserDetailsDto?>
{
    public async Task<UserDetailsDto?> HandleAsync(GetUserByEmailQuery query, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Users
            .AsNoTracking()
            .Include(q => q.Role)
            .Where(q => q.Email == query.Email.ToLowerInvariant())
            .Select(q => q.AsDetailsDto())
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}