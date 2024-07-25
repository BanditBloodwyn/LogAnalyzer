using LogAnalyzer.Core;
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Models.Framework;

public class ComponentInitializer
{
    public static ComponentBase[] InitializeComponents(IServiceCollection services)
    {
        ComponentBase[] components = TypeLoader
            .LoadAssembliesTypesByBase("LogAnalyzer.Components", typeof(ComponentBase))
            .Select(static type => (ComponentBase)Activator.CreateInstance(type)!)
            .ToArray();

        PerformDependencyInjection(components, services);
        ReactToDependencyInjection(components);

        return components;
    }

    private static void PerformDependencyInjection(ComponentBase[] components, IServiceCollection services)
    {
        foreach (ComponentBase component in components)
        {
            if (component is IDependencyInjectionComponent dependencyComponent)
                dependencyComponent.RegisterDependencies(services);
        }

        foreach (ComponentBase component in components)
            component.SetServiceProvider(services.BuildServiceProvider(true));
    }

    private static void ReactToDependencyInjection(ComponentBase[] components)
    {
        foreach (ComponentBase component in components)
        {
            if (component is IReactToDIComponent reactToDiComponent)
                reactToDiComponent.OnDIFinished();
        }
    }
}