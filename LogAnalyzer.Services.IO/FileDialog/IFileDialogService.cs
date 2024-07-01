namespace LogAnalyzer.Services.IO.FileDialog;

public interface IFileDialogService
{
    public Task<string[]> OpenFileDialogAsync();
}