using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.CreateCustomer;

internal class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(q => q.Email)
            .NotEmpty().IsRequiredMessage();
    }
}