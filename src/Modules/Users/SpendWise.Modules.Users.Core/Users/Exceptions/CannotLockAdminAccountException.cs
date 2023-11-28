using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class CannotLockAdminAccountException() : SpendWiseException($"Cannot lock admin account.")
{
    
}