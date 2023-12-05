using SpendWise.Modules.Expenses.Application.Tags.Exceptions;
using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Core.Expenses.Repositories;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Modules.Expenses.Core.Tags.Repositories;

namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Create;

internal class CreateExpenseHandler(IExpenseRepository expenseRepository, ILogger<CreateExpenseHandler> logger,
    IContext context, ITagRepository tagRepository) : ICommandHandler<CreateExpenseCommand, CreateResponse>
{
    public async Task<CreateResponse> HandleAsync(CreateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        var category = AvailableExpenseCategories.GetCategory(command.Category);
        var currency = AvailableCurrencies.GetCurrency(command.Currency);
        var customerId = context.Identity.Id;


        foreach (var tagId in command.TagIds)
            if (!await tagRepository.DoesExistAsync(tagId, customerId, cancellationToken))
                throw new TagNotFound(tagId);

        var expense = Expense.Create(customerId, command.Date, command.Amount, command.Description, category, currency);
        expense.AddTags(command.TagIds);

        var result = await expenseRepository.AddAsync(expense, cancellationToken);
        logger.LogInformation($"Expense with Id: '{result}' has been created by customer with Id: '{customerId}'.");

        return new CreateResponse(result);
    }
}