using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UpdateCustomer;

internal class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    private const int FullNameMaxLength = 100;
    private const int NickMaxLength = 30;
    private const int NickMinLength = 6;

    public UpdateCustomerValidator()
    {
        RuleFor(q => q.FullName)
            .NotEmpty().IsRequiredMessage()
            .MaximumLength(FullNameMaxLength).MaxLengthMessage(FullNameMaxLength);

        RuleFor(q => q.Nick)
            .NotEmpty().IsRequiredMessage()
            .MinimumLength(NickMinLength).MinLengthMessage(NickMinLength)
            .MaximumLength(NickMaxLength).MaxLengthMessage(NickMaxLength);
    }
}