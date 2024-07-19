using System.Collections.ObjectModel;
using LogAnalyzer.Core.Components;

namespace LogAnalyzer.Models.Framework;

public class Framework
{
    private readonly ModuleCache _moduleCache = new();

    #region Singleton

    private static readonly Lazy<Framework> lazy =
        new Lazy<Framework>(() => new Framework());
    public static Framework Instance => lazy.Value;

    #endregion

    public ObservableCollection<MainViewComponent> MainViewModules { get; } = [];

    private Framework()
    {
        ComponentBase[] modules = ModuleInitializer.InitializeModules();
        _moduleCache.AddModules(modules);

        FillMainViewModelsCollection(modules);
    }

    private void FillMainViewModelsCollection(ComponentBase[] modules)
    {
        foreach (ComponentBase module in modules)
            if (module is MainViewComponent mvm)
                MainViewModules.Add(mvm);
    }
}