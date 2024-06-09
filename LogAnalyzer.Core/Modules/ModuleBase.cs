using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Core.Modules;

public abstract class ModuleBase
{
    private IServiceProvider? _serviceProvider;

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected T GetDependency<T>() where T : class
    {
        if (_serviceProvider == null)
            throw new Exception("ServiceProvider not set");

        return _serviceProvider.GetRequiredService<T>();
    }

    protected object GetDependency(Type type)
    {
        if (_serviceProvider == null)
            throw new Exception("ServiceProvider not set");

        return _serviceProvider.GetRequiredService(type);

    }
}