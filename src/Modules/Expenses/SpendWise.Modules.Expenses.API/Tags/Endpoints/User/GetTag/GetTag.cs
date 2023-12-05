using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Application.Tags.Queries.GetSingle;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Expenses.API.Tags.Endpoints.User.GetTag;

[Route(TagEndpoints.Route)]
[Authorize]
internal class GetTag(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<TagDto>
{
    [HttpGet("{tagId:guid}")]
    [SwaggerOperation(
        Summary = "Get Tag",
        Description = "Get Customer's Tag",
        Tags = new[] { TagEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<TagDto>> HandleAsync(Guid tagId,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.QueryAsync(new GetTagQuery(tagId), cancellationToken);

        return result is null ? NoContent() : Ok(result);
    }
}