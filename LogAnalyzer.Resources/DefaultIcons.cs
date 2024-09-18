using Avalonia.Media.Imaging;

namespace LogAnalyzer.Resources;

public static class DefaultIcons
{
    public static Bitmap SettingsIcon => GetBitmapFromBytes(IconsResource.feature_settings);
    public static Bitmap SettingsIconWhite => GetBitmapFromBytes(IconsResource.feature_settings_white);
    public static Bitmap LogAnalysisIcon => GetBitmapFromBytes(IconsResource.feature_loganalysis);
    public static Bitmap LogAnalysisIconWhite => GetBitmapFromBytes(IconsResource.feature_loganalysis_white);
    public static Bitmap LogInnerMessageIcon => GetBitmapFromBytes(IconsResource.log_hasInnerMessage);
    public static Bitmap LogLongRepoInteraction => GetBitmapFromBytes(IconsResource.log_long);
    public static Bitmap LogVeryLongRepoInteraction => GetBitmapFromBytes(IconsResource.log_veryLong);
    public static Bitmap LogHasCommId => GetBitmapFromBytes(IconsResource.log_hasCommId);

    private static Bitmap GetBitmapFromBytes(byte[] bytes)
    {
        using MemoryStream stream = new(bytes);
        return new Bitmap(stream);
    }
}