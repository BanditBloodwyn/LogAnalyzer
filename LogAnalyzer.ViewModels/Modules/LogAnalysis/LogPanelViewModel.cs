using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogPanelViewModel(
    FileInfoModel _file) 
    : ViewModelBase
{
    public FileInfoModel File { get; } = _file;
   
    public ICommand CloseLogPanelCommand => new CloseLogPanelCommand(this);

    public event Action<LogPanelViewModel>? RequestCloseEvent;

    public void StartLogAnalysisAndDisplay()
    {

    }

    public void RequestClose()
    {
        RequestCloseEvent?.Invoke(this);
    }
}