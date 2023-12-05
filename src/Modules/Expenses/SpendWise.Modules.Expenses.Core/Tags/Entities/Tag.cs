using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Core.Tags.Types;
using SpendWise.Modules.Expenses.Core.Tags.ValueObjects.ColorHex;
using SpendWise.Modules.Expenses.Core.Tags.ValueObjects.Name;
using SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;

namespace SpendWise.Modules.Expenses.Core.Tags.Entities;

internal class Tag
{
    public TagId Id { get; init; }
    public CustomerId CustomerId { get; set; }
    public TagName Name { get; set; }
    public TagColorHex ColorHex { get; set; }
    

    //solution to dotnet ef error
    private Tag()
    {
    }

    private Tag(CustomerId customerId, TagName name, TagColorHex colorHex)
    {
        Id = TagId.Create();
        CustomerId = customerId;
        Name = name;
        ColorHex = colorHex;
    }

    public static Tag Create(Guid customerId, string name, string colorHex)
        => new(customerId, name, colorHex);

    public void Update(string name, string colorHex)
    {
        Name = name;
        ColorHex = colorHex;
    }
}