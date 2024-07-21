using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Models.Data.BaseTypes.Commands;
using LogAnalyzer.Models.Events;
using System.Collections.ObjectModel;
using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.Navigation;

public class CommandsPanelViewModel : ViewModelBase
{
    public ObservableCollection<ProgressCommand> Commands { get; set; } = [];

    public CommandsPanelViewModel()
    {
        EventBinding<AddNewProgressCommandEvent> addNewCommandEventBinding = new(OnAddCommand);
        EventBus<AddNewProgressCommandEvent>.Register(addNewCommandEventBinding);
    }

    private void OnAddCommand(AddNewProgressCommandEvent @event)
    {
        Commands.Add(@event.Command);
    }
}