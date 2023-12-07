using SpendWise.Modules.Expenses.Application.Expenses.Commands.Delete;

namespace SpendWise.Modules.Expenses.API.Expenses.Endpoints.User.DeleteExpense;

[Route(ExpenseEndpoints.Route)]
[Authorize]
internal class DeleteExpense(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult
{
    [HttpDelete("{expenseId:guid}")]
    [SwaggerOperation(
        Summary = "Delete Expense",
        Description = "Delete Expense For Assigned Customer",
        Tags = new[] { ExpenseEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult> HandleAsync(Guid expenseId,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(new DeleteExpenseCommand(expenseId), cancellationToken);
        return NoContent();
    }
}