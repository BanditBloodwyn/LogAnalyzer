﻿using LogAnalyzer.ViewModels.Commands;
using LogAnalyzer.ViewModels.Modules;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.Navigation;

public class ModuleButtonsPanelViewModel(
    LogAnalysisModuleViewModel logAnalysisModule,
    SettingsModuleViewModel settingsModule)
    : ViewModelBase
{
    public ObservableCollection<MainModuleViewModelBase> Modules { get; set; } =
        [logAnalysisModule, settingsModule];

    public ICommand OpenModuleCommand => new OpenModuleCommand();
}