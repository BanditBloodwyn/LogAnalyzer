using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;

namespace LogAnalyzer.ViewModels.Design.Modules.LogAnalysis;

public class LogPanelDesignViewModel() : LogPanelViewModel(new FileInfoModel
{
    Name = "TemplateLog.log",
    Path = @"C:\Test\",
    FullName = @"C:\Test\TemplateLog.log"
});