using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Admin.UnlockUser;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.User.Admin.UnlockUser;

[Route(UserEndpoints.Route)]
[Authorize(UsersModule.Policy)]
internal class UnlockUser(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<UpdateResponse>
{
    [HttpPut("{userId:guid}/unlock")]
    [SwaggerOperation(
        Summary = "Unlock User",
        Description = "Unlock User",
        Tags = new[] { UserEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<UpdateResponse>> HandleAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.SendAsync<UnlockUserCommand, UpdateResponse>(new UnlockUserCommand(userId),
            cancellationToken);
        return Ok(result);
    }
}