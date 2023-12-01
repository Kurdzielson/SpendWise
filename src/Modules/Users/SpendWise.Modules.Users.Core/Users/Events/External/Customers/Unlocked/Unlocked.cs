using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events.External.Customers.Unlocked;

internal record Unlocked(Guid CustomerId) : IEvent;