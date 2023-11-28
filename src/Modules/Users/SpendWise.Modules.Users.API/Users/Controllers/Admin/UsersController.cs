using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Users.Core.Users.Commands.Admin.CreateUser;
using SpendWise.Modules.Users.Core.Users.Commands.Admin.LockUser;
using SpendWise.Modules.Users.Core.Users.Commands.Admin.UnlockUser;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Modules.Users.Core.Users.Queries.BrowseUsers;
using SpendWise.Modules.Users.Core.Users.Queries.GetUser;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Users.API.Users.Controllers.Admin;

[Authorize(UsersModule.Policy)]
[Route("users")]
internal class UsersController(IDispatcher dispatcher) : BaseController
{
    [HttpPost("create")]
    [SwaggerOperation("Create User")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateUserAsync(CreateUserCommand command)
    {
        await dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpGet("{userId:guid}")]
    [SwaggerOperation("Get User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailsDto>> GetAsync(Guid userId)
        => OkOrNotFound(await dispatcher.QueryAsync(new GetUserQuery(userId)));

    [HttpGet]
    [SwaggerOperation("Browse users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Paged<UserDto>>> BrowseUsersAsync([FromQuery] BrowseUsersQuery query)
        => Ok(await dispatcher.QueryAsync(query));

    [HttpPut("{userId:guid}/lock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> LockUserAsync(Guid userId)
    {
        await dispatcher.SendAsync(new LockUserCommand(userId));
        return Ok();
    }

    [HttpPut("{userId:guid}/unlock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UnlockUserAsync(Guid userId)
    {
        await dispatcher.SendAsync(new UnlockUserCommand(userId));
        return Ok();
    }
}