using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;

public class LogPanelDesignViewModel : LogPanelViewModel
{
    public LogPanelDesignViewModel()
    {
        File = new FileInfo(
            "2024-07-11_Framework.log",
            @"C:\Test\",
            @"C:\Test\2024-07-11_Framework.log");

        LogEntries.Add(new LogEntry(
            "07/12/2024 09:18:21,216",
            "CoreConfigAccessor",
            "Info",
            @"Connection configuration file: 'D:\GIT\DotNet\Products\AtbasNET\Framework\Start\bin\Debug\Atbas.Framework.Connection.cfg'",
            ""
        ));
        LogEntries.Add(new LogEntry(
            "07/12/2024 09:18:21,392",
            "DatabaseConfiguration",
            "Warning",
            "'CommandTimeout' not found for DatabaseConfiguration",
            "Writing back '' as default value.\rtest\rbla\rbla\rbla\rbla\rbla\rbla\rbla\rbla\rbla\rbla\rbla\rbla\rbla\rbla"
        ));
        LogEntries.Add(new LogEntry(
            "07/12/2024 09:18:21,392",
            "DatabaseConfiguration",
            "Error",
            "'CommandTimeout' not found for DatabaseConfiguration",
            "Writing back '' as default value."
        ));
        OnPropertyChanged(nameof(LogEntries));
    }
}