namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterBuilder
{
    public static bool BuildFilter(FilterData? filter, LogEntryViewModel logEntry)
    {
        if (filter == null)
            return true;

        bool matchesType = ShowType(filter, logEntry);
        bool matchesSpecialFilter = MatchesSpecialFilter(filter, logEntry);
        bool show = ApplyTextFilters(filter.toShow, logEntry);
        bool hide = ApplyTextFilters(filter.toHide, logEntry, false);

        if (!matchesType)
            return false;
        if (!matchesSpecialFilter)
            return false;
        return show && !hide;
    }

    private static bool ShowType(FilterData filter, LogEntryViewModel logEntry)
    {
        if (logEntry.LogType == null)
            return false;

        return filter.logTypeFilters.All(static typeFilter => !typeFilter.Value) ||
               filter.logTypeFilters.GetValueOrDefault(logEntry.LogType.Value, true);
    }

    private static bool MatchesSpecialFilter(FilterData filter, LogEntryViewModel logEntry)
    {
        if (filter.specialFilters.All(static val => !val.Value))
            return true;

        string[] relevantFilterStrings = filter.specialFilters
            .Where(static fil => fil.Value)
            .Select(static fil => fil.Key)
            .ToArray();

        bool contains = relevantFilterStrings.Any(showString =>
                            !string.IsNullOrEmpty(logEntry.Source) &&
                            logEntry.Source.Contains(showString, StringComparison.OrdinalIgnoreCase)) ||
                        relevantFilterStrings.Any(showString =>
                            !string.IsNullOrEmpty(logEntry.Message) &&
                            logEntry.Message.Contains(showString, StringComparison.OrdinalIgnoreCase)) ||
                        relevantFilterStrings.Any(showString =>
                            !string.IsNullOrEmpty(logEntry.InnerMessage) &&
                            logEntry.InnerMessage.Contains(showString, StringComparison.OrdinalIgnoreCase));
        return contains;
    }

    private static bool ApplyTextFilters(string[] textFilters, LogEntryViewModel logEntry, bool @default = true)
    {
        if (textFilters.All(string.IsNullOrEmpty))
            return @default;

        string[] relevantFilterStrings = textFilters.Where(static text => !string.IsNullOrEmpty(text)).ToArray();

        bool contains = relevantFilterStrings.Any(showString =>
                            !string.IsNullOrEmpty(logEntry.Source) &&
                            logEntry.Source.Contains(showString, StringComparison.OrdinalIgnoreCase)) ||
                        relevantFilterStrings.Any(showString =>
                            !string.IsNullOrEmpty(logEntry.Message) &&
                            logEntry.Message.Contains(showString, StringComparison.OrdinalIgnoreCase)) ||
                        relevantFilterStrings.Any(showString =>
                            !string.IsNullOrEmpty(logEntry.InnerMessage) &&
                            logEntry.InnerMessage.Contains(showString, StringComparison.OrdinalIgnoreCase));
        return contains;
    }
}