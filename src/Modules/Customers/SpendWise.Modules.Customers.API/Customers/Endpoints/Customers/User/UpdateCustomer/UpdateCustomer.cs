using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.UpdateCustomer;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.User.UpdateCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize]
internal class UpdateCustomer(IDispatcher dispatcher, IContext context)
    : EndpointBaseAsync
        .WithRequest<UpdateCustomerCommand>
        .WithActionResult
{
    [HttpPut("update")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Update Customer",
        Description = "Update Customer Assigned To Logged Account",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(UpdateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        command.Bind(q => q.CustomerId, context.Identity.Id);
        await dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }
}