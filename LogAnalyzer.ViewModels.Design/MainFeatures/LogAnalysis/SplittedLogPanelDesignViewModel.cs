using Atbas.Core.Logging;
using Atbas.Core.Logging.Reader;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;

public class SplittedLogPanelDesignViewModel : SplittedLogPanelViewModel
{
    public SplittedLogPanelDesignViewModel() : base(null!, null!)
    {
        Cache.Reset();
        Cache.OpenedFiles.Add(new FileInfo(
            0,
            "2024-07-11_Framework.log",
            @"C:\Test\",
            @"C:\Test\2024-07-11_Framework.log"));
        Cache.OpenedFiles.Add(new FileInfo(
            1,
            "2024-07-12_Backbone.log",
            @"C:\Test\",
            @"C:\Test\2024-07-12_Backbone.log"));

        Cache.LogEntries.Add(new LogEntry(
            1,
            0,
            new LogMessage(new DateTime(2024, 12, 7, 9, 18, 21) + TimeSpan.FromMilliseconds(392),
                MessageType.Debug,
                "WebCommunicatorLogger",
                "[4] Query to http://de123450msup17.de123450.vw-group.com:7700/bbvcd/DeliveryData",
                "<MESSAGE DTD=\"XMLMSG\" VERSION=\"1.4.0.0\">\r\n  <COMMAND>\r\n    <REQUEST NAME=\"GetDeliveryNote\" DTD=\"DeliveryData\" VERSION=\"1.4.0.0\" ID=\"2\">\r\n      <PARAM NAME=\"COUNTRY_CODE\" VALUE=\"DEU\" />\r\n      <PARAM NAME=\"DEALER\" VALUE=\"32444\" />\r\n      <PARAM NAME=\"REGION\" VALUE=\"150\" />\r\n      <PARAM NAME=\"BRAND_ID\" VALUE=\"0\" />\r\n      <PARAM NAME=\"CONTROL\" VALUE=\"WORK\" />\r\n    </REQUEST>\r\n  </COMMAND>\r\n</MESSAGE>")));
        Cache.LogEntries.Add(new LogEntry(
            2,
            0,
            new LogMessage(new DateTime(2024, 12, 7, 9, 18, 21) + TimeSpan.FromMilliseconds(216),
                MessageType.Info,
                "CoreConfigAccessor",
                @"Connection configuration file: 'D:\GIT\DotNet\Products\AtbasNET\Framework\Start\bin\Debug\Atbas.Framework.Connection.cfg'",
                "")));
        Cache.LogEntries.Add(new LogEntry(
            3,
            1,
            new LogMessage(new DateTime(2024, 12, 7, 9, 18, 21) + TimeSpan.FromMilliseconds(392),
                MessageType.Warning,
                "LexcomRemoteToken",
                "Share '\\\\TFS\\po_etord' not disconnected",
                "System.ComponentModel.Win32Exception: Diese Netzwerkverbindung ist nicht vorhanden.")));
        Cache.LogEntries.Add(new LogEntry(
            4,
            0,
            new LogMessage(new DateTime(2024, 12, 7, 9, 18, 21) + TimeSpan.FromMilliseconds(392),
                MessageType.Error,
                "CommissioningScannerCommandProcessor",
                "[3] Unable to parse XML from response.",
                "Atbas.FakeDMS.Services.DealerNotFoundException: Dealer not found\r\n   at Atbas.FakeDMS.Services.IngoBackbone.IngoBackboneDMSMethod.GetDealerInformation(XmlDocument request) in D:\\GIT\\DotNet\\Tools\\Fakes\\FakeDMS\\FakeDMS\\Services\\IngoBackbone\\IngoBackboneDMSMethod.cs:line 71\r\n   at Atbas.FakeDMS.Services.IngoBackbone.DeliveryData.GetDeliveryNoteMethod.FillResponse(XmlDocument request, XmlElement responsenode) in D:\\GIT\\DotNet\\Tools\\Fakes\\FakeDMS\\FakeDMS\\Services\\IngoBackbone\\DeliveryData\\GetDeliveryNoteMethod.cs:line 34\r\n   at Atbas.FakeDMS.Services.IngoBackbone.IngoBackboneDMSMethod.Query(XmlDocument request) in D:\\GIT\\DotNet\\Tools\\Fakes\\FakeDMS\\FakeDMS\\Services\\IngoBackbone\\IngoBackboneDMSMethod.cs:line 37\r\n   at Atbas.FakeDMS.Services.IngoBackbone.IngoBackboneDMSService.Query(XmlDocument request) in D:\\GIT\\DotNet\\Tools\\Fakes\\FakeDMS\\FakeDMS\\Services\\IngoBackbone\\IngoBackboneDMSService.cs:line 36\r\n   at Atbas.FakeDMS.Services.IngoBackbone.IngoBackboneDMSService.QueryService(Uri address, String query) in D:\\GIT\\DotNet\\Tools\\Fakes\\FakeDMS\\FakeDMS\\Services\\IngoBackbone\\IngoBackboneDMSService.cs:line 46\r\n   at Atbas.FakeDMS.Services.DMS.Send(Uri uri, String query) in D:\\GIT\\DotNet\\Tools\\Fakes\\FakeDMS\\FakeDMS\\Services\\DMS.cs:line 76\r\n   at Atbas.FakeDMS.HttpServer.DMSHttpServer.HandleRequests(HttpRequest request) in D:\\GIT\\DotNet\\Tools\\Fakes\\FakeDMS\\FakeDMS\\HttpServer\\DMSHttpServer.cs:line 122")));
    }
}