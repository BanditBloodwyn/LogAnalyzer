using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.Settings;

public class SettingsMainDesignViewModel : MainFeatureViewModelBase
{
    public override int NavigationIndex => 0;
    public override string FeatureHeader => "Settings";
    public override Bitmap FeatureIcon => DefaultIcons.SettingsIconWhite;
}