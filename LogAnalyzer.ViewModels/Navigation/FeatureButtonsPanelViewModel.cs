using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Navigation;

public class FeatureButtonsPanelViewModel(
    params MainFeatureViewModelBase[] features)
    : ViewModelBase
{
    public ObservableCollection<MainFeatureViewModelBase> Features { get; set; } = new(features);

    public ICommand OpenFeatureCommand => new OpenFeatureCommand();
}