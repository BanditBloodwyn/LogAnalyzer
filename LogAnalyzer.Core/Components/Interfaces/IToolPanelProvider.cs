using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.Core.Components.Interfaces;

public interface IToolPanelProvider
{
    public ViewModelBase ToolPanel { get; }
}