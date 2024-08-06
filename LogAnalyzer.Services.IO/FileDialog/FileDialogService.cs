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

        List<FileInfo> fileInfos = [];
        for (int i = 0; i < files.Count; i++)
        {
            IStorageFile file = files[i];
            fileInfos.Add(new FileInfo(i, file.Name, file.Path.LocalPath, file.Path.AbsolutePath));
        }

        return fileInfos;
    }
}