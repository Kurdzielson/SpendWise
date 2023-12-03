using System.ComponentModel;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Modules.Users.Core.Users.Queries.GetUser;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Endpoints.Account.User.GetMyAccount;

[Route(AccountEndpoints.Route)]
internal class GetMyAccount(IDispatcher dispatcher, IContext context) : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<UserDetailsDto>
{
    [Authorize]
    [HttpGet("me")]
    [SwaggerOperation(
        Summary = "Gey My Account",
        Description = "Get Logged Account Data",
        Tags = new[] { AccountEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<UserDetailsDto>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.QueryAsync(new GetUserQuery(context.Identity.Id), cancellationToken);
        if (result is not null)
            return Ok(result);
        return NotFound();
    }
}