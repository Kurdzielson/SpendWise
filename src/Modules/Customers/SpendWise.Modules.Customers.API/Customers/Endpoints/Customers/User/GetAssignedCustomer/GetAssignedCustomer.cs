using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Modules.Customers.Core.Customers.Queries.GetCustomer;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.User.GetAssignedCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize]
internal class GetAssignedCustomer(IDispatcher dispatcher, IContext context)
    : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<CustomerDetailsDto>
{
    [HttpGet]
    [Authorize]
    [SwaggerOperation(
        Summary = "Get Customer",
        Description = "Get Customer Assigned To Account",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<CustomerDetailsDto>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.QueryAsync(new GetCustomerQuery(context.Identity.Id), cancellationToken);

        if (result is null)
            return NoContent();
        return Ok(result);
    }
}