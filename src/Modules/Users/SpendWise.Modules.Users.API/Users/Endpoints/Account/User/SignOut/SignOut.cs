using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignOut;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Cookies;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.Account.User.SignOut;

[Route(AccountEndpoints.Route)]
internal class SignOut(IDispatcher dispatcher, IContext context, ICookieService cookieService,
    CookieOptions cookieOptions) : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult
{
    [Authorize]
    [HttpDelete("sign-out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation(
        Summary = "Sign Out",
        Description = "Sign Out From Logged Account",
        Tags = new[] { AccountEndpoints.Tag })]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(new SignOutCommand(context.Identity.Id), cancellationToken);
        cookieService.Remove(AccountEndpoints.AccessTokenCookie, cookieOptions);
        return NoContent();
    }
}