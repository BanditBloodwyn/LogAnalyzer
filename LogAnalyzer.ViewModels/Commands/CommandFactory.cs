using LogAnalyzer.ViewModels.Commands.LogAnalysis;

namespace LogAnalyzer.ViewModels.Commands;

public class CommandFactory
{
    public delegate LogAnalyzeCommand CreateLogAnalyzeCommand();
}