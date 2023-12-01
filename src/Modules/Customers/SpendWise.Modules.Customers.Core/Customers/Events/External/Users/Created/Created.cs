using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Created;

internal record Created(Guid UserId, string Email, string Role) : IEvent;