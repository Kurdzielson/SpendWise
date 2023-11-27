using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Users.Core.Users.Queries.GetUser;

internal record GetUserQuery(Guid UserId) : IQuery<UserDetailsDto?>;