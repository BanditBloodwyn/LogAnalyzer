using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Modules;
using LogAnalyzer.Resources;

namespace LogAnalyzer.ViewModels.MainComponents;

public class SettingsModuleViewModel(SettingsModel _model) : MainModuleViewModelBase
{
    public override int NavigationIndex => 1;
    public override string ModuleHeader => "Settings";
    public override Bitmap ModuleIcon => DefaultIcons.SettingsIcon;
}