using LogAnalyzer.ViewModels.Commands;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.MergedView;

public class MergedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory)
    : LogPanelBaseViewModel(_commandFactory);