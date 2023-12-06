using SpendWise.Modules.Expenses.Application.Expenses.Exceptions;
using SpendWise.Modules.Expenses.Core.Expenses.Repositories;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;

namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Update;

internal class UpdateExpenseHandler(IExpenseRepository expenseRepository, IContext context,
        ILogger<UpdateExpenseHandler> logger)
    : ICommandHandler<UpdateExpenseCommand, UpdateResponse>
{
    public async Task<UpdateResponse> HandleAsync(UpdateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        var currency = AvailableCurrencies.GetCurrency(command.Currency);
        var category = AvailableExpenseCategories.GetCategory(command.Category);

        var customerId = context.Identity.Id;
        var expense = await expenseRepository.GetAsync(command.ExpenseId, customerId, cancellationToken)
                      ?? throw new ExpenseNotFoundException(command.ExpenseId);

        expense.Update(command.Date, command.Amount, command.Description, currency, category);

        await expenseRepository.UpdateAsync(expense, cancellationToken);
        logger.LogInformation($"Expense with Id: '{expense.Id}' has been updated by Customer with Id: '{customerId}'.");

        return new UpdateResponse(expense.Id);
    }
}