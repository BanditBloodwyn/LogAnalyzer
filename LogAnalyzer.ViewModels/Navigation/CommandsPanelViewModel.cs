using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;
using System.Collections.ObjectModel;

namespace LogAnalyzer.ViewModels.Navigation;

public class CommandsPanelViewModel : ViewModelBase
{
    public ObservableCollection<ProgressCommandViewModel> Commands { get; set; } = [];

    public CommandsPanelViewModel()
    {
        EventBinding<AddNewProgressCommandEvent> addNewCommandEventBinding = new(OnAddCommand);
        EventBus<AddNewProgressCommandEvent>.Register(addNewCommandEventBinding);
    }

    private void OnAddCommand(AddNewProgressCommandEvent @event)
    {
        ProgressCommandViewModel commandVM = new(@event.Command);
        commandVM.ProgressFinished += vm =>
        {
            lock (Commands)
            {
                Commands.Remove(vm);
            }
        };
        
        Commands.Add(commandVM);

        commandVM.Start();
    }
}