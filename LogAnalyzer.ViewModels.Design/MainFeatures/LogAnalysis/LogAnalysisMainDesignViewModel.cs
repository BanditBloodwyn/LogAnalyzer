using Avalonia.Media.Imaging;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;

public class LogAnalysisMainDesignViewModel : MainFeatureViewModelBase, IToolPanelProvider
{
    public override int NavigationIndex => 0;
    public override string FeatureHeader => "Log Analysis";
    public override Bitmap FeatureIcon => DefaultIcons.LogAnalysisIconWhite;

    public ViewModelBase ToolPanel { get; } = new LogAnalysisToolPanelDesignViewModel();
}