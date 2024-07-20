using Avalonia.Controls;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.Views.Views.Navigation
{
    public partial class ModuleButtonsPanel : UserControl
    {
        public ModuleButtonsPanel()
        {
            InitializeComponent();

            DataContext = new ModuleButtonsPanelViewModel();
        }
    }
}
