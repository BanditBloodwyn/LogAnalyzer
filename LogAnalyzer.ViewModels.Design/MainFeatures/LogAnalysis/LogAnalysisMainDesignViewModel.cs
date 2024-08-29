using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;

public class LogAnalysisMainDesignViewModel : MainFeatureViewModelBase
{
    public override int NavigationIndex => 0;
    public override string FeatureHeader => "Log Analysis";
    public override Bitmap FeatureIcon => DefaultIcons.LogAnalysisIcon;
}