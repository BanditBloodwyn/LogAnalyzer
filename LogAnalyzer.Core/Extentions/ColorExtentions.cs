using Avalonia.Media;

namespace LogAnalyzer.Core.Extentions;

public static class ColorExtentions
{
    public static Color WithOpacity(this Color color, double opacity)
    {
        return new Color(
            (byte)(255 * opacity),
            color.R,
            color.G,
            color.B
        );
    }
}