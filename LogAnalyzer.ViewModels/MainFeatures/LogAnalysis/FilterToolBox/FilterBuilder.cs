namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterBuilder
{
    public static bool BuildFilter(FilterData? filter, LogEntryViewModel logEntry)
    {
        if (filter == null)
            return true;

        bool show = ApplyTextFilters(filter.toShow, logEntry);
        bool hide = ApplyTextFilters(filter.toHide, logEntry, false);

        return show && !hide;
    }

    private static bool ApplyTextFilters(string[] textFilters, LogEntryViewModel logEntry, bool @default = true)
    {
        if (textFilters.All(string.IsNullOrEmpty))
            return @default;

        string[] relevantFilterStrings = textFilters.Where(text => !string.IsNullOrEmpty(text)).ToArray();

        bool contains = relevantFilterStrings.Any(showString => logEntry.Source.Contains(showString, StringComparison.OrdinalIgnoreCase)) ||
                          relevantFilterStrings.Any(showString => logEntry.Message.Contains(showString, StringComparison.OrdinalIgnoreCase)) ||
                          relevantFilterStrings.Any(showString => logEntry.InnerMessage.Contains(showString, StringComparison.OrdinalIgnoreCase));
        return contains;
    }
}