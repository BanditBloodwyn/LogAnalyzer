using System.Reflection;
using LogAnalyzer.Core;
using LogAnalyzer.Core.Modules;
using LogAnalyzer.Core.Modules.Interfaces;
using LogAnalyzer.Core.Repositories.Json;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Modules.BackendExtentions;

public class RepositoryModule : ModuleBase,
    IDependencyInjectionModule, IReactToDIModule
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