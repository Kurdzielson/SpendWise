using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Admin.LockUser;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.User.Admin.LockUser;

[Route(UserEndpoints.Route)]
[Authorize(UsersModule.Policy)]
internal class LockUser(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    [HttpPut("{userId:guid}/lock")]
    [SwaggerOperation(
        Summary = "Lock User",
        Description = "Lock User",
        Tags = new[] { UserEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(new LockUserCommand(userId), cancellationToken);
        return NoContent();
    }
}