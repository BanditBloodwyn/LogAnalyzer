using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public interface ILogPanel
{
    public void OpenFiles(FileInfo[] filesToOpen);
    }