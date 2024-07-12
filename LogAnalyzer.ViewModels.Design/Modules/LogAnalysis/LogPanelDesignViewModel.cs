using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Modules.LogAnalysis;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;

namespace LogAnalyzer.ViewModels.Design.Modules.LogAnalysis;

public class LogPanelDesignViewModel : LogPanelViewModel
{
    public LogPanelDesignViewModel()
    {
        File = new FileInfoModel()
        {
            Name = "TemplateLog.log",
            Path = @"C:\Test\",
            FullName = @"C:\Test\TemplateLog.log"
        };

        AnalysisProgressPercents = 50;

        LogEntries.Add(new LogEntryModel { TimeStamp = new DateTime() });
        OnPropertyChanged(nameof(LogEntries));
        LogEntries.Add(new LogEntryModel { TimeStamp = new DateTime(2024,7,12) });
        OnPropertyChanged(nameof(LogEntries));
    }
}