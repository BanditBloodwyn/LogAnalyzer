using LogAnalyzer.Core;
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Models.Framework;

public class ComponentInitializer
{
    public static ComponentBase[] InitializeComponents(IServiceCollection services)
    {
        ComponentBase[] modules = TypeLoader
            .LoadAssembliesTypesByBase("LogAnalyzer.Components", typeof(ComponentBase))
            .Select(static type => (ComponentBase)Activator.CreateInstance(type)!)
            .ToArray();

        PerformDependencyInjection(modules, services);
        ReactToDependencyInjection(modules);

        return modules;
    }

    private static void PerformDependencyInjection(ComponentBase[] modules, IServiceCollection services)
    {
        foreach (ComponentBase module in modules)
        {
            if (module is IDependencyInjectionComponent dependencyModule)
                dependencyModule.RegisterDependencies(services);
        }

        foreach (ComponentBase module in modules)
            module.SetServiceProvider(services.BuildServiceProvider(true));
    }

    private static void ReactToDependencyInjection(ComponentBase[] modules)
    {
        foreach (ComponentBase module in modules)
        {
            if (module is IReactToDIComponent reactToDiModule)
                reactToDiModule.OnDIFinished();
        }
    }
}