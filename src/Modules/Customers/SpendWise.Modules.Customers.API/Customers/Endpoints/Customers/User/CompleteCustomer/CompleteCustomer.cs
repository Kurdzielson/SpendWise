using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.CompleteCustomer;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using SpendWise.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Endpoints.Customers.User.CompleteCustomer;

[Route(CustomerEndpoints.Route)]
[Authorize]
internal class CompleteCustomer(IDispatcher dispatcher, IContext context)
    : EndpointBaseAsync
        .WithRequest<CompleteCustomerCommand>
        .WithActionResult<UpdateResponse>
{
    [HttpPut("complete")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Complete Customer",
        Description = "Complete Customer Assigned To Logged Account",
        Tags = new[] { CustomerEndpoints.Tag })]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<UpdateResponse>> HandleAsync(CompleteCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        command.Bind(q => q.CustomerId, context.Identity.Id);
        var result = await dispatcher.SendAsync<CompleteCustomerCommand, UpdateResponse>(command, cancellationToken);
        return Ok(result);
    }
}