using LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.ViewModels.Design.Navigation;

public class MainNavigationDesignViewModel : MainNavigationViewModel
{
    public MainNavigationDesignViewModel()
        : base(new FeatureButtonsPanelDesignViewModel(), new CommandsPanelDesignViewModel())
    {
        ToolPanelVM = new LogAnalysisToolPanelDesignViewModel();
    }
}