using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.CreateCustomer;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.Public.CreateCustomer;

[Route(CustomerEndpoints.Route)]
internal class CreateCustomer(IDispatcher dispatcher) : EndpointBaseAsync
    .WithRequest<CreateCustomerCommand>
    .WithActionResult
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Customer",
        Description = "Create Customer Based On User",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(CreateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }
}