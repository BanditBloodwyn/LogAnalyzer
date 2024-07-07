using Avalonia.Platform.Storage;
using LogAnalyzer.Core.UI;
using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.Services.IO.FileDialog;

public class FileDialogService : IFileDialogService
{
    public async Task<IEnumerable<FileInfoModel>> OpenFileDialogAsync()
    {
        var files = await TopLevelContext.Current
            .StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                AllowMultiple = true,
                Title = "Open log files",
            });

        return files
            .Select(static file => new FileInfoModel
            {
                Name = file.Name,
                Path = file.Path.AbsolutePath,
            })
            .ToArray();
    }
}