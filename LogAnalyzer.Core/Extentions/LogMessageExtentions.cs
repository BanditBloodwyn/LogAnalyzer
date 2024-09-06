using Atbas.Core.Logging;
using Atbas.Core.Logging.Reader;

namespace LogAnalyzer.Core.Extentions;

public static unsafe class LogMessageExtentions
{
    public static long GetBytesSize(this LogMessage message)
    {
        long size = 0;
        size += message.Details?.Length ?? 0;
        size += message.Message.Length;
        size += message.Sender.Length;
        size += sizeof(MessageType);
        size += sizeof(DateTime);
        size += message.Duration?.Length ?? 0;

        return size;
    }
}