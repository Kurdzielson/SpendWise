using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Application.Tags.Queries.GetPaginated;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Expenses.API.Tags.Endpoints.User.GetTags;

[Route(TagEndpoints.Route)]
[Authorize]
internal class GetTags(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<GetTagsQuery>
        .WithActionResult<Paged<TagDto>>
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get Tags",
        Description = "Get Paginated Customer's Tags",
        Tags = new[] { TagEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<Paged<TagDto>>> HandleAsync([FromQuery] GetTagsQuery request,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.QueryAsync(request, cancellationToken);

        if (result is null)
            return NoContent();
        return Ok(result);
    }
}