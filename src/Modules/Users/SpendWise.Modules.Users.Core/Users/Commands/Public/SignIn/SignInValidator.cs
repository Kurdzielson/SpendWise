using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignIn;

internal class SignInValidator : AbstractValidator<SignInCommand>
{
    private const int PasswordMaxLength = 100;
    private const int PasswordMinLength = 6;

    public SignInValidator()
    {
        RuleFor(q => q.Email)
            .NotNull().IsRequiredMessage()
            .NotEmpty().IsRequiredMessage();

        RuleFor(q => q.Password)
            .NotNull().IsRequiredMessage()
            .NotEmpty().IsRequiredMessage()
            .MinimumLength(PasswordMinLength).MinLengthMessage(PasswordMinLength)
            .MaximumLength(PasswordMaxLength).MaxLengthMessage(PasswordMaxLength);
    }
}