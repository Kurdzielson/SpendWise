namespace SpendWise.Modules.Users.Core.Users;

public class RegistrationOptions
{
    public bool Enabled { get; set; }
    public IEnumerable<string> InvalidEmailProviders { get; set; }
}