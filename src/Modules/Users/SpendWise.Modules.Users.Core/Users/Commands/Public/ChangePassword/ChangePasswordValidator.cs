using System.Data;
using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.ChangePassword;

internal class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    private const int PasswordMaxLength = 100;
    private const int PasswordMinLength = 6;

    public ChangePasswordValidator()
    {
        RuleFor(q => q.OldPassword)
            .NotEmpty().IsRequiredMessage();

        RuleFor(q => q.NewPassword)
            .NotEmpty().IsRequiredMessage()
            .MinimumLength(PasswordMinLength).MinLengthMessage(PasswordMinLength)
            .MaximumLength(PasswordMaxLength).MaxLengthMessage(PasswordMaxLength);

        RuleFor(q => q.ConfirmNewPassword)
            .NotEmpty().IsRequiredMessage()
            .Equal(q => q.NewPassword).WithMessage("NewPassword should match ConfirmNewPassword");
    }
}