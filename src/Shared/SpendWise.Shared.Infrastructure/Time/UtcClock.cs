using SpendWise.Shared.Abstraction.Time;

namespace SpendWise.Shared.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
    public DateTimeOffset CurrentDateTimeOffset() => DateTimeOffset.UtcNow;
}