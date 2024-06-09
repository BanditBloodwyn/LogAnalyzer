﻿using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace LogAnalyzer.Core.Modules;

public abstract class MainViewModule : ModuleBase
{
    public abstract int NavigationIndex { get; }
    public abstract string ModuleHeader { get; }
    public virtual Bitmap? ModuleIcon { get; } = null;

    public abstract UserControl GetView();
}