using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;

internal class VerifyCustomerValidator : AbstractValidator<VerifyCustomerCommand>
{
    public VerifyCustomerValidator()
    {
        RuleFor(q => q.CustomerId)
            .NotEmpty().IsRequiredMessage();
    }
}