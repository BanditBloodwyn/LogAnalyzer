using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Modules;
using LogAnalyzer.Resources;

namespace LogAnalyzer.ViewModels.MainFeatures;

public class SettingsMainViewModel(SettingsModel _model) : MainModuleViewModelBase
{
    public override int NavigationIndex => 1;
    public override string ModuleHeader => "Settings";
    public override Bitmap ModuleIcon => DefaultIcons.SettingsIcon;
}