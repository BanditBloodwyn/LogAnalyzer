using Avalonia.Media.Imaging;
using LogAnalyzer.Resources.Icons;

namespace LogAnalyzer.Resources;

public static class DefaultIcons
{
    public static Bitmap SettingsIcon => GetBitmapFromBytes(IconsResource.feature_settings);
    public static Bitmap LogAnalysisIcon => GetBitmapFromBytes(IconsResource.feature_loganalysis);
    public static Bitmap LogInnerMessageIcon => GetBitmapFromBytes(IconsResource.log_hasInnerMessage);

    private static Bitmap GetBitmapFromBytes(byte[] bytes)
    {
        using MemoryStream stream = new(bytes);
        return new Bitmap(stream);
    }
}