﻿namespace LogAnalyzer.Models.Data.Containers;

public readonly record struct LogEntry(
    long index,
    string timeStamp,
    string source,
    string logType,
    string message,
    string innerMessage,
    int fileIndex,
    RepositoryInteractionInformation? repositoryInteractionInformation = null);