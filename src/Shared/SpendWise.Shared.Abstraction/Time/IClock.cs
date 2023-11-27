namespace SpendWise.Shared.Abstraction.Time;

public interface IClock
{
    DateTime CurrentDate();
    DateTimeOffset CurrentDateTimeOffset();
}