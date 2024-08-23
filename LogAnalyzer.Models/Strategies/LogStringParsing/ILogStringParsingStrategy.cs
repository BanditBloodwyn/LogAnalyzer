﻿using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.Models.Strategies.LogStringParsing;

public interface ILogStringParsingStrategy
{
    public Task<LogEntry> ParseLogString(
        long logEntryIndex,
        string logString,
        CancellationToken cancellationToken,
        int fileIndex);
}