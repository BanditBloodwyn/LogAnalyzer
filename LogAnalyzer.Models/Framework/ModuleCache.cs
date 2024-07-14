using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Extentions;

namespace LogAnalyzer.Models.Framework;

public class ModuleCache
{
    private readonly List<ComponentBase> _modules = [];

    public void AddModule(ComponentBase module)
    {
        if (!_modules.Contains(module))
            _modules.Add(module);
    }

    public void AddModules(IEnumerable<ComponentBase> components)
    {
        if (_modules.ContainsNone(components))
            _modules.AddRange(components);
    }

    public IEnumerable<ComponentBase> GetAllModules()
    {
        return _modules;
    }
}