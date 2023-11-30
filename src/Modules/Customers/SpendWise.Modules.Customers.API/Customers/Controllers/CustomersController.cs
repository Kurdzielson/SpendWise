using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Modules.Customers.Core.Customers.Commands.CompleteCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.CreateCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.UpdateCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;
using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Modules.Customers.Core.Customers.Queries.BrowseCustomers;
using SpendWise.Modules.Customers.Core.Customers.Queries.GetCustomer;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SpendWise.Modules.Customers.API.Customers.Controllers;

[ApiController]
[Route("customers")]
internal class CustomersController(IDispatcher dispatcher, IContext context) : Controller
{
    [HttpPost]
    [SwaggerOperation("Create Customer")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateCustomerAsync(CreateCustomerCommand command)
    {
        await dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPut("complete")]
    [Authorize]
    [SwaggerOperation("Create Customer")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CompleteCustomerAsync(CompleteCustomerCommand command)
    {
        command.Bind(q => q.CustomerId, context.Identity.Id);
        await dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPut("verify")]
    [Authorize]
    [SwaggerOperation("Verify Customer")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> VerifyCustomerAsync()
    {
        var command = new VerifyCustomerCommand(context.Identity.Id);
        await dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPut("update")]
    [Authorize]
    [SwaggerOperation("Update Customer")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateCustomerAsync(UpdateCustomerCommand command)
    {
        command.Bind(q => q.CustomerId, context.Identity.Id);
        await dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPut("{customerID:guid}/lock")]
    [Authorize(CustomerModule.Policy)]
    [SwaggerOperation("Lock Customer")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> LockCustomerAsync(Guid customerId)
    {
        await dispatcher.SendAsync(new LockCustomerCommand(customerId));
        return NoContent();
    }

    [HttpPut("{customerID:guid}/unlock")]
    [Authorize(CustomerModule.Policy)]
    [SwaggerOperation("Unlock Customer")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UnlockCustomerAsync(Guid customerId)
    {
        await dispatcher.SendAsync(new UnlockCustomerCommand(customerId));
        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [SwaggerOperation("Get Customer Assigned To Account")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailsDto>> GetCustomerAsync()
    {
        var result = await dispatcher.QueryAsync(new GetCustomerQuery(context.Identity.Id));

        if (result is null)
            return NoContent();
        return Ok(result);
    }

    [HttpGet("browse")]
    [Authorize(CustomerModule.Policy)]
    [SwaggerOperation("Browse Customers")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailsDto>> BrowseCustomersAsync([FromQuery] BrowseCustomersQuery query)
        => Ok(await dispatcher.QueryAsync(query));

    [HttpGet("{customerId:guid}")]
    [Authorize(CustomerModule.Policy)]
    [SwaggerOperation("Get Customer")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailsDto>> GetCustomerAsync(Guid customerId)
    {
        var result = await dispatcher.QueryAsync(new GetCustomerQuery(customerId));

        if (result is null)
            return NoContent();
        return Ok(result);
    }
}