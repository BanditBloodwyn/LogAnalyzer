using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Modules;

namespace LogAnalyzer.ViewModels.Modules;

public class SettingsModuleViewModel(SettingsModel _model) : ViewModelBase,
    IReactToOpeningViewModel
{
    public void OnShown()
    {
        ;
    }
}