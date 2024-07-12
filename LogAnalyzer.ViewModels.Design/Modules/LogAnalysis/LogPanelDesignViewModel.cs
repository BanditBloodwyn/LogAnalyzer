using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.ViewModels.Design.Modules.LogAnalysis;

public class LogPanelDesignViewModel()
{
    public FileInfoModel? File { get; } = new FileInfoModel
    {
        Name = "TemplateLog.log",
        Path = @"C:\Test\",
        FullName = @"C:\Test\TemplateLog.log"
    };
}