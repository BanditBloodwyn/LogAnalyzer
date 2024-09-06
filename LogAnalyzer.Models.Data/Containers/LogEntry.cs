using Atbas.Core.Logging.Reader;

namespace LogAnalyzer.Models.Data.Containers;

public readonly record struct LogEntry(
    long LogIndex,
    int FileIndex,
    LogMessage? LogMessage,
    RepositoryInteractionInformation? RepositoryInteractionInformation = null);