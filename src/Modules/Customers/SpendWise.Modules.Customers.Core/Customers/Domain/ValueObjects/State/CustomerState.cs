using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects;

namespace SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;

internal class CustomerState : ValueObject
{
    public string Code { get; set; }

    public CustomerState(string code)
    {
        if (!IsCodeSupported(code))
            throw new UnsupportedCustomerStateCodeException(code);

        Code = code;
    }

    private static bool IsCodeSupported(string code)
        => AvailableCustomerStateCodes.AllCodes.Contains(code, StringComparer.InvariantCultureIgnoreCase);
}