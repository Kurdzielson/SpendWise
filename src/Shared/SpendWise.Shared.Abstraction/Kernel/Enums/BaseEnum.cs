namespace SpendWise.Shared.Abstraction.Kernel.Enums;

public class BaseEnum
{
    public string Name { get; set; }
    public int Id { get; set; }

    private BaseEnum(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public static BaseEnum Map(Enum @enum)
        => new(@enum.ToString(), Convert.ToInt32(@enum));
}