using System;
using Microsoft.Extensions.Logging;


namespace HSC.RTD.AVLAggregatorCore.Logging
{
    public interface IAvlLogger<T> where T : class
    {
        void LogDebug(EventId logEvent, int sessionId, string message, params object[] args);
        void LogInformation(EventId logEvent, int sessionId, string message, params object[] args);
        void LogError(Exception ex, int sessionId, string message,  params object[] args);
    }
}