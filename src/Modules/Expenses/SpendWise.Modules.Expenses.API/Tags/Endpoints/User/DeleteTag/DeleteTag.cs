using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Expenses.Application.Tags.Commands.Delete;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Expenses.API.Tags.Endpoints.User.DeleteTag;

[Route(TagEndpoints.Route)]
[Authorize]
internal class DeleteTag(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult
{
    [HttpDelete("{tagId:guid}")]
    [SwaggerOperation(
        Summary = "Delete Tag",
        Description = "Delete Assigned Customer's Tag",
        Tags = new[] { TagEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(Guid tagId,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(new DeleteTagCommand(tagId), cancellationToken);
        return NoContent();
    }
}