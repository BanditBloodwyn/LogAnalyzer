using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Core.Modules.Interfaces;

public interface IDependencyInjectionModule
{
    public void RegisterDependencies(IServiceCollection service);
}