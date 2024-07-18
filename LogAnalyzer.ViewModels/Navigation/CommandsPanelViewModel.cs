using System.Collections.ObjectModel;

namespace LogAnalyzer.ViewModels.Navigation;

public class CommandsPanelViewModel : ViewModelBase
{
    public ObservableCollection<string> Commands { get; set; } = ["test", "tsrg"];
}