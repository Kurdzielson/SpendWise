using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description;

namespace SpendWise.Modules.Expenses.Application.Expenses.DTO;

internal class ExpenseDetailsDto : ExpenseDto
{
    public Guid CustomerId { get; set; }
    public ExpenseDescription Description { get; set; }
    public IEnumerable<TagDto> Tags { get; set; }
}