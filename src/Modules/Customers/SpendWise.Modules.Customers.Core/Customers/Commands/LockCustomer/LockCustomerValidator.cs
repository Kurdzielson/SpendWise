using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;

internal class LockCustomerValidator : AbstractValidator<LockCustomerCommand>
{
    public LockCustomerValidator()
    {
        RuleFor(q => q.CustomerId)
            .NotEmpty().IsRequiredMessage();
    }
}