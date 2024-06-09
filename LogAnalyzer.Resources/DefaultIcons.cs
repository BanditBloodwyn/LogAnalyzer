using Avalonia.Media.Imaging;
using LogAnalyzer.Resources.Icons;

namespace LogAnalyzer.Resources;

public static class DefaultIcons
{
    public static Bitmap SettingsIcon => GetBitmapFromBytes(IconsResource.module_settings);

    private static Bitmap GetBitmapFromBytes(byte[] bytes)
    {
        using MemoryStream stream = new(bytes);
        return new Bitmap(stream);
    }
}