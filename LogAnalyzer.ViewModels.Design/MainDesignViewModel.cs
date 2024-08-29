using LogAnalyzer.ViewModels.Design.Navigation;

namespace LogAnalyzer.ViewModels.Design;

public class MainDesignViewModel : MainViewModel
{
    public MainDesignViewModel()
    {
        NavigationViewModel = new MainNavigationDesignViewModel();
    }
}