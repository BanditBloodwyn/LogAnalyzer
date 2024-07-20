using Avalonia.Media.Imaging;

namespace LogAnalyzer.Core.ViewsModels;

public abstract class MainModuleViewModelBase : ViewModelBase
{
    public abstract int NavigationIndex { get; }
    public abstract string ModuleHeader { get; }
    public abstract Bitmap ModuleIcon { get; }

}