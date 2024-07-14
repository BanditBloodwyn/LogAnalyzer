using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Core.Components.Interfaces;

public interface IDependencyInjectionComponent
{
    public void RegisterDependencies(IServiceCollection service);
}