namespace SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;

internal class AvailableUserStateCodes
{
    public static readonly string Active = nameof(Active);
    public static readonly string Locked = nameof(Locked);

    internal static readonly IReadOnlyCollection<string> AllCodes = new List<string>()
    {
        Active, Locked
    };
}