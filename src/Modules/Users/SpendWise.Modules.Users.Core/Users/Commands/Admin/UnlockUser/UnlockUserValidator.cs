using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.UnlockUser;

internal class UnlockUserValidator : AbstractValidator<UnlockUserCommand>
{
    public UnlockUserValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty().IsRequiredMessage();
    }
}