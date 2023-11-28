using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Public.ChangePassword;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignIn;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignOut;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignUp;
using SpendWise.Modules.Users.Core.Users.Domain.Services;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Modules.Users.Core.Users.Queries.GetUser;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Controllers.Public;

[Route("accounts")]
internal class AccountController(IDispatcher dispatcher, IContext context, IUserRequestStorage userRequestStorage,
        CookieOptions cookieOptions)
    : BaseController
{
    private const string AccessTokenCookie = "__access-token";

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign Up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign In")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDetailsDto>> SignInAsync(SignInCommand command)
    {
        await dispatcher.SendAsync(command);
        var jwt = userRequestStorage.GetToken(command.Id);
        var user = await dispatcher.QueryAsync(new GetUserQuery(jwt.UserId));
        AddCookie(AccessTokenCookie, jwt.AccessToken);
        return Ok(user);
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation("Get Account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDetailsDto>> GetAsync()
        => OkOrNotFound(await dispatcher.QueryAsync(new GetUserQuery(context.Identity.Id)));

    [Authorize]
    [HttpPut("change-password")]
    [SwaggerOperation("Change password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ChangePasswordAsync(ChangePasswordCommand command)
    {
        await dispatcher.SendAsync(command.Bind(q => q.UserId, context.Identity.Id));
        return NoContent();
    }

    [Authorize]
    [HttpDelete("sign-out")]
    [SwaggerOperation("Sign Out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> SignOutAsync()
    {
        await dispatcher.SendAsync(new SignOutCommand(context.Identity.Id));
        DeleteCookie(AccessTokenCookie);
        return NoContent();
    }

    private void AddCookie(string key, string value)
        => Response.Cookies.Append(key, value, cookieOptions);

    private void DeleteCookie(string key)
        => Response.Cookies.Delete(key, cookieOptions);
}