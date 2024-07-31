using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;

public class MergedLogPanelDesignViewModel : MergedLogPanelViewModel
{
    public MergedLogPanelDesignViewModel() : base(null!)
    {
        OpenedFiles.Add(new FileInfo(
            "2024-07-11_Framework.log",
            @"C:\Test\",
            @"C:\Test\2024-07-11_Framework.log"));
        OpenedFiles.Add(new FileInfo(
            "2024-07-12_Backbone.log",
            @"C:\Test\",
            @"C:\Test\2024-07-12_Backbone.log"));
    }
}