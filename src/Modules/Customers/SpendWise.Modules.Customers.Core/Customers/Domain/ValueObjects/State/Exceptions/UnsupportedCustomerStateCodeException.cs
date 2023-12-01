using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State.Exceptions;

internal class UnsupportedCustomerStateCodeException : SpendWiseException
{
    public string Code { get; set; }
    public UnsupportedCustomerStateCodeException(string code) : base($"Unsupported state code: {code}.")
    {
        Code = code;
    }
}