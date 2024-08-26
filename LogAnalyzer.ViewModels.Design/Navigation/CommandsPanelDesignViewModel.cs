using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.ViewModels.Design.Navigation;

public class CommandsPanelDesignViewModel : CommandsPanelViewModel
{
    public CommandsPanelDesignViewModel()
    {
        Commands.Add(new ProgressCommandDesignViewModel());
    }
}