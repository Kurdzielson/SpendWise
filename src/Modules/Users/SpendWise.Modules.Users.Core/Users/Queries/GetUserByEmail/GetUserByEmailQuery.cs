using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Users.Core.Users.Queries.GetUserByEmail;

internal record GetUserByEmailQuery(string Email) : IQuery<UserDetailsDto?>;