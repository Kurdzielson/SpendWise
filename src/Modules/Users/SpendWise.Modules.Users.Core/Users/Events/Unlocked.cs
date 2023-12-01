using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events;

internal record Unlocked(Guid UserId) : IEvent;