using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogPanelViewModel(FileInfoModel _file) : ViewModelBase
{
    public FileInfoModel File { get; } = _file;
}