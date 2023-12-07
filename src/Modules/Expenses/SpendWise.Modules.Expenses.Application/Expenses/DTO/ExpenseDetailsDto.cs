using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description;

namespace SpendWise.Modules.Expenses.Application.Expenses.DTO;

internal class ExpenseDetailsDto : ExpenseDto
{
    public Guid CustomerId { get; set; }
    public string Description { get; set; }
}