using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State.Extensions;

internal class UnsupportedUserStateException(string code) : SpendWiseException($"Unsupported User State Code : '{code}")
{
    public string Code { get; set; } = code;
}