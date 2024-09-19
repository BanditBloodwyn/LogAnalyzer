namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

public static class ContextMenuProvider
{
    public static string[] ContextMenuContent(int clickedColumn)
    {
        return clickedColumn switch
        {
            0 => ["1", "2"],
            1 => ["9", "8"],
            2 => ["test", "bläää"],
            _ => ["hää?"]
        };
    }
}