using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignUp;

internal class SingUpValidator : AbstractValidator<SignUpCommand>
{
    private const int PasswordMaxLength = 100;
    private const int PasswordMinLength = 6;

    public SingUpValidator()
    {
        RuleFor(q => q.Email)
            .NotNull().IsRequiredMessage()
            .NotEmpty().IsRequiredMessage();

        RuleFor(q => q.Password)
            .NotNull().IsRequiredMessage()
            .NotEmpty().IsRequiredMessage()
            .MinimumLength(PasswordMinLength).MinLengthMessage(PasswordMinLength)
            .MaximumLength(PasswordMaxLength).MaxLengthMessage(PasswordMaxLength);

        RuleFor(q => q.ConfirmPassword)
            .NotNull().IsRequiredMessage()
            .NotEmpty().IsRequiredMessage()
            .Equal(a => a.Password).WithMessage("Passwords do not match");
    }
}