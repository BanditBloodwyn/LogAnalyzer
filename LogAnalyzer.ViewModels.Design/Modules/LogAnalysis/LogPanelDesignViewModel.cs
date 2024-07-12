using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Modules.LogAnalysis;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;

namespace LogAnalyzer.ViewModels.Design.Modules.LogAnalysis;

public class LogPanelDesignViewModel : LogPanelViewModel
{
    public LogPanelDesignViewModel()
    {
        File = new FileInfoModel
        {
            Name = "2024-07-11_Framework.log",
            Path = @"C:\Test\",
            FullName = @"C:\Test\2024-07-11_Framework.log"
        };

        LogEntries.Add(new LogEntryModel
        {
            TimeStamp = new DateTime(2024,7,12,14,4,13,655),
            Source = "CoreConfigAccessor",
            LogType = "Info",
            Message = @"Connection configuration file: 'D:\GIT\DotNet\Products\AtbasNET\Framework\Start\bin\Debug\Atbas.Framework.Connection.cfg'"
        });
        LogEntries.Add(new LogEntryModel
        {
            TimeStamp = new DateTime(2024,7,12,14,4,13,696),
            Source = "DatabaseConfiguration",
            LogType = "Info",
            Message = "'CommandTimeout' not found for DatabaseConfiguration",
            InnerMessage = "Writing back '' as default value."
        });
        OnPropertyChanged(nameof(LogEntries));

        AnalysisProgressPercents = 50;
    }
}