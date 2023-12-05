using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Expenses.Application.Tags.Commands.Create;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Expenses.API.Tags.Endpoints.User.CreateTag;

[Route(TagEndpoints.Route)]
[Authorize]
internal class CreateTag(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<CreateTagCommand>
        .WithActionResult<CreateResponse>
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Tag",
        Description = "Create Tag For Assigned Customer",
        Tags = new[] { TagEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<CreateResponse>> HandleAsync(CreateTagCommand request,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.SendAsync<CreateTagCommand, CreateResponse>(request, cancellationToken);
        return Ok(result);
    }
}