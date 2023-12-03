using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.Admin.UnlockCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize(CustomerModule.Policy)]
internal class UnlockCustomer(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<UpdateResponse>
{
    [HttpPut("{customerId:guid}/unlock")]
    [SwaggerOperation(
        Summary = "Unlock Customer",
        Description = "Unlock Customer",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<UpdateResponse>> HandleAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var result =
            await dispatcher.SendAsync<UnlockCustomerCommand, UpdateResponse>(new UnlockCustomerCommand(customerId),
                cancellationToken);
        return Ok(result);
    }
}