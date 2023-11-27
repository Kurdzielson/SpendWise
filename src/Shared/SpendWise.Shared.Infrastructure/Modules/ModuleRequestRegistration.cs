namespace SpendWise.Shared.Infrastructure.Modules;

internal sealed class ModuleRequestRegistration(Type requestType, Type responseType,
    Func<object, CancellationToken, Task<object>> action)
{
    public Type RequestType { get; } = requestType;
    public Type ResponseType { get; } = responseType;
    public Func<object, CancellationToken, Task<object>> Action { get; } = action;
}