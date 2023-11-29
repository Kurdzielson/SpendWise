namespace SpendWise.Modules.Users.Core.Users.Domain.Entities;

internal class Role
{
    public string Name { get; set; }
    public IEnumerable<string> Permissions { get; set; }
    public IEnumerable<User> Users { get; set; }

    public static string Default => User;
    public const string User = "user";
    public const string Admin = "admin";

    //solution to dotnet ef error
    private Role()
    {
    }

    private Role(string name, IEnumerable<string> permissions)
    {
        Name = name;
        Permissions = permissions;
    }

    public static Role Create(string name, IEnumerable<string> permissions)
        => new(name, permissions);
}