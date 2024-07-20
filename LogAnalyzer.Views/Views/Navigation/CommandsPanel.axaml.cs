using Avalonia.Controls;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.Views.Views.Navigation
{
    public partial class CommandsPanel : UserControl
    {
        public CommandsPanel()
        {
            InitializeComponent();

            DataContext = new CommandsPanelViewModel();
        }
    }
}
