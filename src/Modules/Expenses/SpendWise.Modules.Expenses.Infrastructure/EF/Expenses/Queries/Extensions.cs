using SpendWise.Modules.Expenses.Application.Expenses.DTO;
using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Read.Model;
using SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Queries;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Queries;

internal static class Extensions
{
    public static ExpenseDto AsDto(this ExpenseReadModel expense)
        => expense.Map<ExpenseDto>();

    public static ExpenseDetailsDto AsDetailsDto(this ExpenseReadModel expense, IEnumerable<TagDto> tags)
    {
        var dto = expense.Map<ExpenseDetailsDto>();
        dto.CustomerId = expense.CustomerId;
        dto.Description = expense.Description;
        dto.Tags = tags;

        return dto;
    }

    private static T Map<T>(this ExpenseReadModel expense) where T : ExpenseDto, new()
        => new()
        {
            ExpenseId = expense.Id,
            Amount = expense.Amount,
            Category = AvailableExpenseCategories.GetCategory(expense.Category),
            Currency = AvailableCurrencies.GetCurrency(expense.Currency),
            Date = expense.Date
        };
}