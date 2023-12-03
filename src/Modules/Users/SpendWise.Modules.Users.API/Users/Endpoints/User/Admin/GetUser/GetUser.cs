using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Modules.Users.Core.Users.Queries.GetUser;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.User.Admin.GetUser;

[Route(UserEndpoints.Route)]
[Authorize(UsersModule.Policy)]
internal class GetUser(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<UserDetailsDto>
{
    [HttpGet("{userId:guid}")]
    [SwaggerOperation(
        Summary = "Get User",
        Description = "Get User",
        Tags = new[] { UserEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<UserDetailsDto>> HandleAsync(Guid userId,
        CancellationToken cancellationToken = default)
        => Ok(await dispatcher.QueryAsync(new GetUserQuery(userId), cancellationToken));
}