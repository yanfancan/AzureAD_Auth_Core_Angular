using Microsoft.Extensions.Logging;

namespace HSC.RTD.AVLAggregatorCore.Logging
{
    public static class AvlLogEvent
    {
        public static readonly EventId AvlRequest = new EventId(1, "AvlRequest");
        public static readonly EventId AvlResponse = new EventId(2, "AvlResponse");
        public static readonly EventId LastPositionCall = new EventId(3, "LastPositionCall");
        public static readonly EventId LoginCall = new EventId(4, "LoginCall");
        public static readonly EventId LogoutCall = new EventId(5, "LogoutCall");
        public static readonly EventId ExportDataCall = new EventId(6, "ExportDataCall");
    }
}