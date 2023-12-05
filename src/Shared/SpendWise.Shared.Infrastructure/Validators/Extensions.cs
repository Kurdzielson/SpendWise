using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SpendWise.Shared.Infrastructure.Validators;

public static class Extensions
{
    public static IServiceCollection AddValidators(this IServiceCollection service, IEnumerable<Assembly> assemblies)
        => service.Scan(q => q.FromAssemblies(assemblies)
            .AddClasses(x => x.AssignableTo(typeof(IValidator<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
}