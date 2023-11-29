namespace SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;

internal class AvailableCustomerStateCodes
{
    public static readonly string New = nameof(New);
    public static readonly string Completed = nameof(Completed);
    public static readonly string Verified = nameof(Verified);
    public static readonly string Locked = nameof(Locked);

    internal static readonly IReadOnlyCollection<string> AllCodes = new List<string>()
    {
        New, Completed, Verified, Locked
    };
}