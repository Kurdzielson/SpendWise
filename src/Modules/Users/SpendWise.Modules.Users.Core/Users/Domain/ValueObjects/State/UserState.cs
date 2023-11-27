using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State.Extensions;

namespace SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;

internal class UserState
{
    public string Code { get; }

    public UserState(string code)
    {
        if (IsCodeSupported(code))
            throw new UnsupportedUserStateException(code);

        Code = code;
    }

    private static bool IsCodeSupported(string code)
        => AvailableUserStateCodes.AllCodes.Contains(code, StringComparer.InvariantCultureIgnoreCase);
}