using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.MainFeatures;
using LogAnalyzer.Resources;

namespace LogAnalyzer.ViewModels.MainFeatures;

public class SettingsMainViewModel(SettingsModel _model) : MainFeatureViewModelBase
{
    public override int NavigationIndex => 1;
    public override string FeatureHeader => "Settings";
    public override Bitmap FeatureIcon => DefaultIcons.SettingsIcon;
}