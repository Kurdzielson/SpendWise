using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Core.Expenses.Repositories;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;

namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Create;

internal class CreateExpenseHandler(IExpenseRepository expenseRepository, ILogger<CreateExpenseHandler> logger,
    IContext context) : ICommandHandler<CreateExpenseCommand, CreateResponse>
{
    public async Task<CreateResponse> HandleAsync(CreateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        var category = AvailableExpenseCategories.GetCategory(command.Category);
        var currency = AvailableCurrencies.GetCurrency(command.Currency);
        var customerId = context.Identity.Id;

        var expense = Expense.Create(customerId, command.Name, command.Date, command.Amount, command.Description,
            category, currency);

        var result = await expenseRepository.AddAsync(expense, cancellationToken);
        logger.LogInformation($"Expense with Id: '{result}' has been created by customer with Id: '{customerId}'.");

        return new CreateResponse(result);
    }
}