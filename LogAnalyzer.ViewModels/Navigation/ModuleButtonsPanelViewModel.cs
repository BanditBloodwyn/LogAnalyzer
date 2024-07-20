using LogAnalyzer.Core.Components;
using LogAnalyzer.Models.Framework;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Navigation;

public class ModuleButtonsPanelViewModel : ViewModelBase
{
    private readonly FrameworkModel _frameworkModel = new();

    public ObservableCollection<MainViewComponent> MainViewModules => _frameworkModel.MainViewModules;

    public ICommand OpenModuleCommand => new OpenModuleCommand();

}