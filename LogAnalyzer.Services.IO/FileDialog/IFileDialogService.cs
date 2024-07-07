using Avalonia.Platform.Storage;

namespace LogAnalyzer.Services.IO.FileDialog;

public interface IFileDialogService
{
    public Task<IEnumerable<IStorageFile>> OpenFileDialogAsync();
}