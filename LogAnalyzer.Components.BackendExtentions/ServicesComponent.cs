using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Services.IO.FileDialog;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.BackendExtentions;

public class ServicesComponent : ComponentBase,
    IDependencyInjectionComponent
{
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddSingleton<IFileDialogService, FileDialogService>();
    }
}