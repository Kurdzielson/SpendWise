using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Public.ChangePassword;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.Account.User.ChangeAccountPassword;

[Route(AccountEndpoints.Route)]
internal class ChangeAccountPassword(IDispatcher dispatcher, IContext context)
    : EndpointBaseAsync
        .WithRequest<ChangePasswordCommand>
        .WithActionResult
{
    [Authorize]
    [HttpPut("change-password")]
    [SwaggerOperation(
        Summary = "Change Password",
        Description = "Change Logged User Account Password",
        Tags = new[] { AccountEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(ChangePasswordCommand request,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(request.Bind(q => q.UserId, context.Identity.Id), cancellationToken);
        return NoContent();
    }
}