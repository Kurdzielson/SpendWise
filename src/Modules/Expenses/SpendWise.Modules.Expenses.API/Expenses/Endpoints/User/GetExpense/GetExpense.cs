using SpendWise.Modules.Expenses.Application.Expenses.DTO;
using SpendWise.Modules.Expenses.Application.Expenses.Queries.GetSingle;

namespace SpendWise.Modules.Expenses.API.Expenses.Endpoints.User.GetExpense;

[Route(ExpenseEndpoints.Route)]
[Authorize]
internal class GetExpense(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<ExpenseDetailsDto>
{
    [HttpGet("{expenseId:guid}")]
    [SwaggerOperation(
        Summary = "Get Tag",
        Description = "Get Customer's Tag",
        Tags = new[] { ExpenseEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<ExpenseDetailsDto>> HandleAsync(Guid expenseId,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.QueryAsync(new GetExpenseQuery(expenseId), cancellationToken);
        return Ok(result);
    }
}