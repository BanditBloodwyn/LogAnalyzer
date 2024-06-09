using System.Collections.ObjectModel;
using LogAnalyzer.Core.Modules;

namespace LogAnalyzer.Models.Framework;

public class FrameworkModel
{
    private readonly ModuleCache _moduleCache = new();

    public ObservableCollection<MainViewModule> MainViewModules { get; } = [];

    public FrameworkModel()
    {
        ModuleBase[] modules = ModuleInitializer.InitializeModules();
        _moduleCache.AddModules(modules);

        FillMainViewModelsCollection(modules);
    }

    private void FillMainViewModelsCollection(ModuleBase[] modules)
    {
        foreach (ModuleBase module in modules)
            if (module is MainViewModule mvm)
                MainViewModules.Add(mvm);
    }
}