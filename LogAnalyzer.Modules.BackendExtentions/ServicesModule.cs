using LogAnalyzer.Core.Modules.Interfaces;
using LogAnalyzer.Core.Modules;
using LogAnalyzer.Services.IO.FileDialog;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Modules.BackendExtentions;

public class ServicesModule : ModuleBase,
    IDependencyInjectionModule
{
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddSingleton<IFileDialogService, FileDialogService>();
    }
}