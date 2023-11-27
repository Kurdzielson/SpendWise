using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class EmailInUseException() : SpendWiseException("Email is already in use.");