using LogAnalyzer.Core.Extentions;
using LogAnalyzer.Core.Modules;

namespace LogAnalyzer.Models.Framework;

public class ModuleCache
{
    private readonly List<ModuleBase> _modules = [];

    public void AddModule(ModuleBase module)
    {
        if (!_modules.Contains(module))
            _modules.Add(module);
    }

    public void AddModules(IEnumerable<ModuleBase> modules)
    {
        if (_modules.ContainsNone(modules))
            _modules.AddRange(modules);
    }

    public IEnumerable<ModuleBase> GetAllModules()
    {
        return _modules;
    }
}