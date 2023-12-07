using SpendWise.Modules.Expenses.Application.Expenses.Commands.Create;

namespace SpendWise.Modules.Expenses.API.Expenses.Endpoints.User.CreateExpense;

[Route(ExpenseEndpoints.Route)]
[Authorize]
internal class CreateExpense(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<CreateExpenseCommand>
        .WithActionResult<CreateResponse>
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Expense",
        Description = "Create Expense For Assigned Customer",
        Tags = new[] { ExpenseEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<CreateResponse>> HandleAsync(CreateExpenseCommand request,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.SendAsync<CreateExpenseCommand, CreateResponse>(request, cancellationToken);
        return result;
    }
}