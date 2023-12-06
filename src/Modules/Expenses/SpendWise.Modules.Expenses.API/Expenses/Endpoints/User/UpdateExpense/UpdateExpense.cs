using SpendWise.Modules.Expenses.Application.Expenses.Commands.Update;
using SpendWise.Shared.Infrastructure.Api;

namespace SpendWise.Modules.Expenses.API.Expenses.Endpoints.User.UpdateExpense;

[Route(ExpenseEndpoints.Route)]
[Authorize]
internal class UpdateExpense(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<UpdateExpenseRequest>
        .WithActionResult<UpdateResponse>
{
    [HttpPut("{expenseId:guid}")]
    [SwaggerOperation(
        Summary = "Update Expense",
        Description = "Update Expense For Assigned Customer",
        Tags = new[] { ExpenseEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<UpdateResponse>> HandleAsync([FromRoute] UpdateExpenseRequest request,
        CancellationToken cancellationToken = default)
    {
        request.Command.Bind(q => q.ExpenseId, request.ExpenseId);
        var result =
            await dispatcher.SendAsync<UpdateExpenseCommand, UpdateResponse>(request.Command, cancellationToken);

        return Ok(result);
    }
}