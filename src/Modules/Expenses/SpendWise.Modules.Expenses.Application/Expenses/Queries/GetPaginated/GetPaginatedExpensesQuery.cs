using SpendWise.Modules.Expenses.Application.Expenses.DTO;

namespace SpendWise.Modules.Expenses.Application.Expenses.Queries.GetPaginated;

internal class GetPaginatedExpensesQuery() : PagedQuery<ExpenseDto>
{
    internal new string OrderBy { get; set; } //hide
}