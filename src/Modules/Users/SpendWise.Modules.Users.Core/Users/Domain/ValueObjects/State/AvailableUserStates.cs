namespace SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;

internal class AvailableUserStates
{
    public static readonly UserState Active = new(AvailableUserStateCodes.Active);
    private static readonly UserState Locked = new(AvailableUserStateCodes.Locked);

    private static readonly HashSet<UserState> AllStates = new()
    {
        Active, Locked
    };
    
    public static UserState Default = Active;

    public static UserState GetState(string code)
        => AllStates.SingleOrDefault(q => q.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
    
    public static IEnumerable<UserState> GetAll()
        => AllStates.ToList();
}