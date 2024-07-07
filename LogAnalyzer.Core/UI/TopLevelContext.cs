using Avalonia.Controls;

namespace LogAnalyzer.Core.UI;

public static class TopLevelContext
{
    private static TopLevel? _currentTopLevel;
    
    public static TopLevel Current => _currentTopLevel ?? throw new InvalidOperationException("TopLevel has not been initialized.");

    public static void Initialize(TopLevel? topLevel) => _currentTopLevel = topLevel;
}