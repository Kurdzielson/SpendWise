using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.Admin.LockCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize(CustomerModule.Policy)]
internal class LockCustomer(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<UpdateResponse>
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
    public override async Task<ActionResult<UpdateResponse>> HandleAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var result =
            await dispatcher.SendAsync<LockCustomerCommand, UpdateResponse>(new LockCustomerCommand(customerId),
                cancellationToken);
        return Ok(result);
    }
}