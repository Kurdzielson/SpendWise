using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;

internal class UnlockCustomerValidator : AbstractValidator<UnlockCustomerCommand>
{
    public UnlockCustomerValidator()
    {
        RuleFor(q => q.CustomerId)
            .NotEmpty().IsRequiredMessage();
    }
}