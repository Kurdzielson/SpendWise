using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Modules.Users.Core.Users.Queries.BrowseUsers;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.User.Admin.BrowseUsers;

[Route(UserEndpoints.Route)]
[Authorize(UsersModule.Policy)]
internal class BrowseUsers(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<BrowseUsersQuery>
    .WithActionResult<Paged<UserDto>>
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Browse Users",
        Description = "Browse Users With Filters",
        Tags = new[] { UserEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<Paged<UserDto>>> HandleAsync([FromQuery] BrowseUsersQuery request,
        CancellationToken cancellationToken = default)
        => Ok(await dispatcher.QueryAsync(request, cancellationToken));
}