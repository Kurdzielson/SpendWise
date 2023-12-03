using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Modules.Customers.Core.Customers.Queries.BrowseCustomers;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.Admin.BrowseCustomers;

[Route(CustomerEndpoints.Route)]
[Authorize(CustomerModule.Policy)]
internal class BrowseCustomers(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<BrowseCustomersQuery>
    .WithActionResult<Paged<CustomerDto>>
{
    [HttpGet("browse")]
    [Authorize(CustomerModule.Policy)]
    [SwaggerOperation(
        Summary = "Browse Customers",
        Description = "Browse Customers With Filters",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<Paged<CustomerDto>>> HandleAsync([FromQuery] BrowseCustomersQuery query,
        CancellationToken cancellationToken = default)
        => Ok(await dispatcher.QueryAsync(query, cancellationToken));
}