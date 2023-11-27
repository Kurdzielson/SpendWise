using SpendWise.Shared.Abstraction.Auth;
using SpendWise.Shared.Abstraction.Storage;

namespace SpendWise.Modules.Users.Core.Users.Domain.Services;

internal sealed class UserRequestStorage(IRequestStorage requestStorage) : IUserRequestStorage
{
    public void SetToken(Guid commandId, JsonWebToken jwt)
        => requestStorage.Set(GetKey(commandId), jwt);

    public JsonWebToken GetToken(Guid commandId)
        => requestStorage.Get<JsonWebToken>(GetKey(commandId));

    private static string GetKey(Guid commandId) => $"jwt:{commandId:N}";
}