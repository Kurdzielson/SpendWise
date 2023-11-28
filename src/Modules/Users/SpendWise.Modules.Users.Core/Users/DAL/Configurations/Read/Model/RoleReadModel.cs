namespace SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read.Model;

internal class RoleReadModel
{
    public string Name { get; init; }
    public IEnumerable<string> Permissions { get; init; }
    public IEnumerable<UserReadModel> Users { get; init; }
}