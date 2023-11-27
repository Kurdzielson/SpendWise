using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class SignUpDisabledException() : SpendWiseException("Sign up is disabled.");