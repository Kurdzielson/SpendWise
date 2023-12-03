using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignUp;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.Account.Public.SIgnUp;

[Route(AccountEndpoints.Route)]
internal class SignUp(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<SignUpCommand>
    .WithActionResult
{
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Create new Account",
        Description = "Create new User Account",
        Tags = new[] { AccountEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(SignUpCommand request,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(request, cancellationToken);
        return NoContent();
    }
}