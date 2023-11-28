using FluentValidation;
using SpendWise.Shared.Abstraction.Validations;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.LockUser;

internal class LockUserValidator : AbstractValidator<LockUserCommand>
{
    public LockUserValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty().IsRequiredMessage();
    }
}