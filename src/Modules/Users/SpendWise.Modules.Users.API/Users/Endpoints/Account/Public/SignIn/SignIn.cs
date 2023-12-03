using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignIn;
using SpendWise.Modules.Users.Core.Users.Domain.Services;
using SpendWise.Modules.Users.Core.Users.Queries.GetUser;
using SpendWise.Shared.Abstraction.Cookies;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.Account.Public.SignIn;

[Route(AccountEndpoints.Route)]
internal class SignIn(IDispatcher dispatcher, IUserRequestStorage userRequestStorage, ICookieService cookieService,
        CookieOptions cookieOptions)
    : EndpointBaseAsync
        .WithRequest<SignInCommand>
        .WithActionResult
{
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sing In",
        Description = "Sing In",
        Tags = new[] { AccountEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(SignInCommand request,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(request, cancellationToken);
        var jwt = userRequestStorage.GetToken(request.Id);
        var user = await dispatcher.QueryAsync(new GetUserQuery(jwt.UserId), cancellationToken);
        cookieService.Set(AccountEndpoints.AccessTokenCookie, jwt.AccessToken, cookieOptions);
        return Ok(user);
    }
}