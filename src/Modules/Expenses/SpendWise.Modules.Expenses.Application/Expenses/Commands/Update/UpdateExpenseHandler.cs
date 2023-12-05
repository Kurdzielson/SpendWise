using SpendWise.Modules.Expenses.Application.Expenses.Exceptions;
using SpendWise.Modules.Expenses.Application.Tags.Exceptions;
using SpendWise.Modules.Expenses.Core.Expenses.Repositories;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Modules.Expenses.Core.Tags.Repositories;

namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Update;

internal class UpdateExpenseHandler(IExpenseRepository expenseRepository, IContext context,
        ILogger<UpdateExpenseHandler> logger, ITagRepository tagRepository)
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

        foreach (var tagId in command.TagIds)
            if (!await tagRepository.DoesExistAsync(tagId, customerId, cancellationToken))
                throw new TagNotFound(tagId);
        
        expense.Update(command.Date, command.Amount, command.Description, currency, category, command.TagIds);

        await expenseRepository.UpdateAsync(expense, cancellationToken);
        logger.LogInformation($"Expense with Id: '{expense.Id}' has been updated by Customer with Id: '{customerId}'.");

        return new UpdateResponse(expense.Id);
    }
}