using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LogAnalyzer.Views.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
