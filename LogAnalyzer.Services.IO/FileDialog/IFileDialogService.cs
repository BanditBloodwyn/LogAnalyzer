using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.Services.IO.FileDialog;

public interface IFileDialogService
{
    public Task<IEnumerable<FileInfoModel>> OpenFileDialogAsync();
}