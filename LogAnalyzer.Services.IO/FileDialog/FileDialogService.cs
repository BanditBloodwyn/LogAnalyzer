using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace LogAnalyzer.Services.IO.FileDialog;

public class FileDialogService : IFileDialogService
{
    private TopLevel level;
    
    public async Task<string[]> OpenFileDialogAsync()
    {
        var files = await level.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            AllowMultiple = true,
            Title = "Open log files"
        });
        
        return files.Select(sel => sel.Name).ToArray();
    }
}