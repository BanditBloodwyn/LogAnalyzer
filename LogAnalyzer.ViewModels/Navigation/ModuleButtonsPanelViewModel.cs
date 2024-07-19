using LogAnalyzer.Core.Components;
using LogAnalyzer.Models.Framework;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Navigation;

public class ModuleButtonsPanelViewModel : ViewModelBase
{
    public ObservableCollection<MainViewComponent> MainViewModules => Framework.Instance.MainViewModules;

    public ICommand OpenModuleCommand => new OpenModuleCommand();
}