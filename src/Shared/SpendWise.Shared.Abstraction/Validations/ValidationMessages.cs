using FluentValidation;

namespace SpendWise.Shared.Abstraction.Validations;

public static class ValidationMessages
{
    public static IRuleBuilderOptions<T, TProperty> IsRequiredMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule)
    {
        rule.WithMessage($"'{{PropertyName}}' is required.");
        return rule;
    }
    
    public static IRuleBuilderOptions<T, TProperty> MaxLengthMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, int length)
    {
        rule.WithMessage($"'{{PropertyName}}' maximum lenght is {length}.");
        return rule;
    }
    
    public static IRuleBuilderOptions<T, TProperty> MinLengthMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, int length)
    {
        rule.WithMessage($"'{{PropertyName}}' minimum lenght is {length}.");
        return rule;
    }
}