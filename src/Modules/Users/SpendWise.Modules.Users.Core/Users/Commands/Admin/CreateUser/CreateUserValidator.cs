using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.CreateUser;

internal class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    private const int PasswordMaxLength = 100;
    private const int PasswordMinLength = 6;

    public CreateUserValidator()
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
            .Equal(q => q.Password).WithMessage("Password should match ConfirmPassword");

        RuleFor(q => q.Role)
            .NotEmpty().IsRequiredMessage();
    }
}