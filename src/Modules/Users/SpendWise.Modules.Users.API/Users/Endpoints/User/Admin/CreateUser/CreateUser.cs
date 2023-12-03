using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Admin.CreateUser;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.User.Admin.CreateUser;

[Route(UserEndpoints.Route)]
[Authorize(UsersModule.Policy)]
internal class CreateUser(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<CreateUserCommand>
    .WithActionResult
{
    [HttpPost("create")]
    [SwaggerOperation(
        Summary = "Create User Account",
        Description = "Create User Account",
        Tags = new[] { UserEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(CreateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(request, cancellationToken);
        return NoContent();
    }
}