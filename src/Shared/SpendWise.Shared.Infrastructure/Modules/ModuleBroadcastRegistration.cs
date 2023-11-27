namespace SpendWise.Shared.Infrastructure.Modules;

internal sealed class ModuleBroadcastRegistration(Type receiverType, Func<object, CancellationToken, Task> action)
{
    public Type ReceiverType { get; } = receiverType;
    public Func<object, CancellationToken, Task> Action { get; } = action;
    public string Key => ReceiverType.Name;
}