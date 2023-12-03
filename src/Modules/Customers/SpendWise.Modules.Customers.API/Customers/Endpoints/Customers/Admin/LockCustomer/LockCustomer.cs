using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.Admin.LockCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize(CustomerModule.Policy)]
internal class LockCustomer(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    [HttpPut("{customerId:guid}/lock")]
    [Authorize(CustomerModule.Policy)]
    [SwaggerOperation(
        Summary = "Lock Customer",
        Description = "Lock Customer",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(new LockCustomerCommand(customerId), cancellationToken);
        return NoContent();
    }
}