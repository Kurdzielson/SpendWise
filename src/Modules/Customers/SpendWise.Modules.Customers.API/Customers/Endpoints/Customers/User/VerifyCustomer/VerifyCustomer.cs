using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.User.VerifyCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize]
internal class VerifyCustomer(IDispatcher dispatcher, IContext context)
    : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
{
    [HttpPut("verify")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Verify Customer",
        Description = "Verify Customer Assigned To Logged Account",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var command = new VerifyCustomerCommand(context.Identity.Id);
        await dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }
}