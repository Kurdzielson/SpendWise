namespace SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;

internal abstract class AvailableCustomerState
{
    public static readonly CustomerState New = new(AvailableCustomerStateCodes.New);
    public static readonly CustomerState Completed = new(AvailableCustomerStateCodes.Completed);
    public static readonly CustomerState Verified = new(AvailableCustomerStateCodes.Verified);
    public static readonly CustomerState Locked = new(AvailableCustomerStateCodes.Locked);

    public static readonly CustomerState DefaultState = New;

    private static readonly HashSet<CustomerState> AllStates = new ()
    {
        New, Completed, Verified, Locked
    };

    public static CustomerState GetState(string code)
        => AllStates.SingleOrDefault(q => q.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));

    public static IEnumerable<CustomerState> GetAll()
        => AllStates.ToList();
}