using Avalonia.Platform.Storage;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogPanelViewModel(IStorageFile _file) : ViewModelBase
{
    public IStorageFile File { get; } = _file;
}