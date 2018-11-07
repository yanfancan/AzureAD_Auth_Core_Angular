using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HSC.RTD.AVLAggregatorCore.Logging
{
    public class AvlLogger<T> : IAvlLogger<T> where T : class
    {
        private readonly ILogger _logger;
        public AvlLogger(ILoggerFactory lf)
        {
            this._logger = lf.CreateLogger<T>();
        }

        public void LogDebug(EventId logEvent, int sessionId, string message, params object[] args)
        {
            _logger.LogDebug(logEvent, $"SessionID: {sessionId}; " + message, args);
        }

        public void LogInformation(EventId logEvent, int sessionId, string message, params object[] args)
        {
            _logger.LogInformation(logEvent, $"SessionID: {sessionId}; " + message, args);
        }

        public void LogError(Exception ex, int sessionId, string message, params object[] args)
        {
            _logger.LogError(ex, $"SessionID: {sessionId}; " + message, args);
        }
    }
}
