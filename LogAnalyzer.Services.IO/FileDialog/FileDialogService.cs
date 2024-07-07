using Avalonia.Platform.Storage;
using LogAnalyzer.Core.UI;

namespace LogAnalyzer.Services.IO.FileDialog;

public class FileDialogService : IFileDialogService
{
    public async Task<IEnumerable<IStorageFile>> OpenFileDialogAsync()
    {
        var files = await TopLevelContext.Current
            .StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                AllowMultiple = true,
                Title = "Open log files",
            });

        return files.ToArray();
    }
}