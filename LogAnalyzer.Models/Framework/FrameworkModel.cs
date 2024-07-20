using System.Collections.ObjectModel;
using LogAnalyzer.Core.Components;

namespace LogAnalyzer.Models.Framework;

public class FrameworkModel
{
    private readonly ModuleCache _moduleCache = new();

    public ObservableCollection<MainViewComponent> MainViewModules { get; } = [];

    public FrameworkModel()
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