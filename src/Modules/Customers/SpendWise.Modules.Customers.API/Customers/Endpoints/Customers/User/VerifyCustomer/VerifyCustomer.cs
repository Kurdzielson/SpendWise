using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.User.VerifyCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize]
internal class VerifyCustomer(IDispatcher dispatcher, IContext context)
    : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<UpdateResponse>
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
    public override async Task<ActionResult<UpdateResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var command = new VerifyCustomerCommand(context.Identity.Id);
        var result = await dispatcher.SendAsync<VerifyCustomerCommand, UpdateResponse>(command, cancellationToken);
        return Ok(result);
    }
}