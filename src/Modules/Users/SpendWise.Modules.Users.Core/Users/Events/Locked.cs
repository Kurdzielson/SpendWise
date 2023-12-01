using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events;

internal record Locked(Guid UserId) : IEvent;