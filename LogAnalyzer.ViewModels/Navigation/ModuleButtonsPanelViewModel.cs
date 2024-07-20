using LogAnalyzer.Core.Components;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Navigation;

public class ModuleButtonsPanelViewModel : ViewModelBase
{
    public ObservableCollection<MainViewComponent> MainViewModules { get; set; }

    public ICommand OpenModuleCommand => new OpenModuleCommand();

}