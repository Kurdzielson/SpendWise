using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Modules.Customers.Core.Customers.Queries.GetCustomer;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.Admin.GetCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize(CustomerModule.Policy)]
internal class GetCustomer(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<CustomerDetailsDto>
{
    [HttpGet("{customerId:guid}")]
    [Authorize(CustomerModule.Policy)]
    [SwaggerOperation(
        Summary = "Get Customer",
        Description = "Get Customer",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<CustomerDetailsDto>> HandleAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.QueryAsync(new GetCustomerQuery(customerId), cancellationToken);

        if (result is null)
            return NoContent();
        return Ok(result);
    }
}