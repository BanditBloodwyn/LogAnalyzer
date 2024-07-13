using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Design.Modules.LogAnalysis;

public class LogPanelDesignViewModel : LogPanelViewModel
{
    public LogPanelDesignViewModel()
    {
        File = new FileInfo(
            "2024-07-11_Framework.log",
            @"C:\Test\",
            @"C:\Test\2024-07-11_Framework.log");

        LogEntries.Add(new LogEntry(
            new DateTime(2024, 7, 12, 14, 4, 13, 655),
            "CoreConfigAccessor",
            "Info",
            @"Connection configuration file: 'D:\GIT\DotNet\Products\AtbasNET\Framework\Start\bin\Debug\Atbas.Framework.Connection.cfg'",
            ""
        ));
        LogEntries.Add(new LogEntry(
            new DateTime(2024, 7, 12, 14, 4, 13, 696),
            "DatabaseConfiguration",
            "Info",
            "'CommandTimeout' not found for DatabaseConfiguration",
            "Writing back '' as default value."
        ));
        OnPropertyChanged(nameof(LogEntries));

        AnalysisProgressPercents = 50;
    }
}