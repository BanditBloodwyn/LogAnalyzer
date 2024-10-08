﻿using Atbas.Core.Logging;
using Atbas.Core.Logging.Reader;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;

public class LogEntryDesignViewModel() : LogEntryViewModel(
    new LogEntry(
    1,
    0,
    new LogMessage(
        new DateTime(2024, 12, 7, 9, 18, 21),
        MessageType.Debug,
        "WebCommunicatorLogger",
        "[4] Query to http://de123450msup17.de123450.vw-group.com:7700/bbvcd/DeliveryData",
        "<MESSAGE DTD=\"XMLMSG\" VERSION=\"1.4.0.0\">\r\n  <COMMAND>\r\n    <REQUEST NAME=\"GetDeliveryNote\" DTD=\"DeliveryData\" VERSION=\"1.4.0.0\" ID=\"2\">\r\n      <PARAM NAME=\"COUNTRY_CODE\" VALUE=\"DEU\" />\r\n      <PARAM NAME=\"DEALER\" VALUE=\"32444\" />\r\n      <PARAM NAME=\"REGION\" VALUE=\"150\" />\r\n      <PARAM NAME=\"BRAND_ID\" VALUE=\"0\" />\r\n      <PARAM NAME=\"CONTROL\" VALUE=\"WORK\" />\r\n    </REQUEST>\r\n  </COMMAND>\r\n</MESSAGE>")), 
    new ContextMenuProvider(null));