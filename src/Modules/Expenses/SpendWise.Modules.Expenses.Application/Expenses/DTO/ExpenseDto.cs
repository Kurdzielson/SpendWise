using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;

namespace SpendWise.Modules.Expenses.Application.Expenses.DTO;

internal class ExpenseDto
{
    public Guid ExpenseId { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public ExpenseCategory Category { get; set; }
    public DateTimeOffset Date { get; set; }
}