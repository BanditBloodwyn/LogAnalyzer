using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterBuilder
{
    public static bool BuildFilter(FilterData? filterData, LogEntryViewModel logEntry)
    {
        if (filterData == null)
            return true;

        if(filterData.filterIsEmpty)
            return true;

        bool matchesCheckboxFilers = MatchesCheckboxFilters(filterData, logEntry);
        bool show = ApplyTextFilters(filterData.toShow, logEntry);
        bool hide = ApplyTextFilters(filterData.toHide, logEntry, false);

        if (!matchesCheckboxFilers)
            return false;
        return show && !hide;
    }

    private static bool MatchesCheckboxFilters(FilterData filterData, LogEntryViewModel logEntry)
    {
        if (filterData.checkboxFilters.All(filter => !filter.Checked))
            return true;

        bool match = false;

        foreach (FilterCheckboxViewModel filter in filterData.checkboxFilters)
        {
            if (!filter.Checked)
                continue;
            match = match || filter.FilterFunction(logEntry);
        }
        return match;
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