using System.Reflection;
using LogAnalyzer.Core;
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Core.Repositories.Json;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.BackendExtentions;

public class RepositoryComponent : ComponentBase,
    IDependencyInjectionComponent, IReactToDIComponent
{
    private Type[]? _repoTypes;

    public void RegisterDependencies(IServiceCollection service)
    {
        _repoTypes = TypeLoader
            .LoadAssembliesTypesByBase("LogAnalyzer.Models.Data", typeof(JsonRepository<>))
            .ToArray();

        foreach (Type repoType in _repoTypes)
            service.AddSingleton(repoType);
    }

    public void OnDIFinished()
    {
        if (_repoTypes == null)
            return;

        foreach (Type type in _repoTypes)
        {
            object repo = GetDependency(type);

            MethodInfo? methodInfo = type.GetMethod("Initialize");
            methodInfo?.Invoke(repo, null);
        }
    }
}