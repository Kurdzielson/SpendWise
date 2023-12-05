using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Expenses.Application.Tags.Commands.Update;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using SpendWise.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Expenses.API.Tags.Endpoints.User.UpdateTag;

[Route(TagEndpoints.Route)]
[Authorize]
internal class UpdateTag(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<UpdateTagRequest>
        .WithActionResult<UpdateResponse>
{
    [HttpPut("{tagId:guid}")]
    [SwaggerOperation(
        Summary = "Update Tag",
        Description = "Update Assigned Customer's Tag",
        Tags = new[] { TagEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<UpdateResponse>> HandleAsync([FromRoute] UpdateTagRequest request,
        CancellationToken cancellationToken = default)
    {
        request.Command.Bind(q => q.TagId, request.TagId);
        var result = await dispatcher.SendAsync<UpdateTagCommand, UpdateResponse>(request.Command, cancellationToken);

        return Ok(result);
    }
}