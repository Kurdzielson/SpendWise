using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Shared.Abstraction.Endpoints.ValueObjects;

namespace SpendWise.Modules.Expenses.API.Expenses.Endpoints.Public.GetCategories;

[Route(ExpenseEndpoints.Route)]
[AllowAnonymous]
internal class GetExpenseCategories : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<List<ExpenseCategory>>
{
    [HttpGet("categories")]
    [SwaggerOperation(
        Summary = "Get Categories",
        Description = "Get All Categories",
        Tags = new[] { ValueObjectEndpoints.Tag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override Task<ActionResult<List<ExpenseCategory>>> HandleAsync(
        CancellationToken cancellationToken = default)
        => Task.FromResult<ActionResult<List<ExpenseCategory>>>(Ok(AvailableExpenseCategories.GetList()));
}