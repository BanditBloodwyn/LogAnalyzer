using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace LogAnalyzer.Core.Components;

public abstract class MainViewComponent : ComponentBase
{
    public abstract int NavigationIndex { get; }
    public abstract string ModuleHeader { get; }
    public virtual Bitmap? ModuleIcon { get; } = null;

    public abstract UserControl GetView();
}