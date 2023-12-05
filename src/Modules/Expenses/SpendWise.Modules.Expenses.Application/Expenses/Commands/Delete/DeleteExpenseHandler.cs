using SpendWise.Modules.Expenses.Application.Expenses.Exceptions;
using SpendWise.Modules.Expenses.Core.Expenses.Repositories;

namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Delete;

internal class DeleteExpenseHandler(IExpenseRepository expenseRepository, ILogger<DeleteExpenseHandler> logger,
    IContext context) : ICommandHandler<DeleteExpenseCommand>
{
    public async Task HandleAsync(DeleteExpenseCommand command, CancellationToken cancellationToken = default)
    {
        var customerId = context.Identity.Id;
        var expense = await expenseRepository.GetAsync(command.ExpenseId, customerId, cancellationToken)
                      ?? throw new ExpenseNotFoundException(command.ExpenseId);

        await expenseRepository.RemoveAsync(expense, cancellationToken);
        logger.LogInformation(
            $"Expense with Id: '{command.ExpenseId}' has been removed by Customer with Id: '{customerId}'");
    }
}