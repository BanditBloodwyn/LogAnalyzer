using Avalonia.Controls;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.Views.Views.Navigation
{
    public partial class MainNavigationView : UserControl
    {
        public MainNavigationView()
        {
            InitializeComponent();

            DataContext = new MainNavigationViewModel();
        }
    }
}
