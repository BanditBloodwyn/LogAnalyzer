using Avalonia.Platform.Storage;
using LogAnalyzer.Core.UI;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.Services.IO.FileDialog;

public class FileDialogService : IFileDialogService
{
    public async Task<IEnumerable<FileInfo>> OpenFileDialogAsync()
    {
        IReadOnlyList<IStorageFile> files = await TopLevelContext.Current
            .StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                AllowMultiple = true,
                Title = "Open log files",
            });

        return files
            .Select(static file => new FileInfo(file.Name, file.Path.LocalPath, file.Path.AbsolutePath))
            .ToArray();
    }
}