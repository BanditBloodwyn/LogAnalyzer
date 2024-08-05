using Avalonia.Media;

namespace LogAnalyzer.Resources;

public class DefaultColors
{
    public static DefaultColors Instance { get; } = new();
    private DefaultColors(){}
    
    public static Color Light => Color.Parse("#eeeeee");
    public static Color White => Color.Parse("#ffffff");
    public static Color Dark => Color.Parse("#222222");
    
    public static Color Primary => Color.Parse("#3d4c49");
    public static Color Secondary => Color.Parse("#3b7496");
    
    public static Color Accent1 => Color.Parse("#08899f");
    public static Color Accent2 => Color.Parse("#cc0000");
    public static Color Accent3 => Color.Parse("#afb0ba");
    
    public static Color Success => Color.Parse("#00aa40");
    public static Color Warning => Color.Parse("#f1c700");
    public static Color Danger => Color.Parse("#f6003a");
    public static Color Info => Color.Parse("#648aa5");
}