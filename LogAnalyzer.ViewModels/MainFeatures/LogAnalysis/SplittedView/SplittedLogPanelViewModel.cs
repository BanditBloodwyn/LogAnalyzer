using LogAnalyzer.ViewModels.Commands;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;

public class SplittedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory)
    : LogPanelBaseViewModel(_commandFactory);