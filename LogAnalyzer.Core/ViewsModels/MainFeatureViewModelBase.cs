using Avalonia.Media.Imaging;

namespace LogAnalyzer.Core.ViewsModels;

public abstract class MainFeatureViewModelBase : ViewModelBase
{
    public abstract int NavigationIndex { get; }
    public abstract string FeatureHeader { get; }
    public abstract Bitmap FeatureIcon { get; }
}