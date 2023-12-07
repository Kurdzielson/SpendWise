using SpendWise.Modules.Expenses.Application.Expenses.DTO;
using SpendWise.Modules.Expenses.Application.Expenses.Queries.GetPaginated;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Expenses.API.Expenses.Endpoints.User.GetPaginatedExpenses;

[Route(ExpenseEndpoints.Route)]
[Authorize]
internal class GetPaginatedExpenses(IDispatcher dispatcher)
    : EndpointBaseAsync
        .WithRequest<GetPaginatedExpensesQuery>
        .WithActionResult<Paged<ExpenseDto>>
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get Paginated Expenses",
        Description = "Get Customer's Paginated Expenses",
        Tags = new[] { ExpenseEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<Paged<ExpenseDto>>> HandleAsync(
        [FromQuery] GetPaginatedExpensesQuery request,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher.QueryAsync(request, cancellationToken);
        return Ok(result);
    }
}