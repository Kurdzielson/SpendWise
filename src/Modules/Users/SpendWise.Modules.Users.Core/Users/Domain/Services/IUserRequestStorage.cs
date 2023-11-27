using SpendWise.Shared.Abstraction.Auth;

namespace SpendWise.Modules.Users.Core.Users.Domain.Services;

internal interface IUserRequestStorage
{
    void SetToken(Guid commandId, JsonWebToken jwt);
    JsonWebToken GetToken(Guid commandId);
}