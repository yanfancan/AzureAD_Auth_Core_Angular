using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using HSC.RTD.AVLAggregatorCore.Logging;

namespace HSC.RTD.AVLAggregatorCore.Middleware
{
    public class RequestInterceptor
    {
        private readonly IAvlLogger<RequestInterceptor> _logger;
        private readonly RequestDelegate _next;

        public RequestInterceptor(RequestDelegate next, IAvlLogger<RequestInterceptor> logger)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestLog = $"REQUEST HttpMethod: {context.Request.Method}, Path: {context.Request.Path}";
            var injectedRequestStream = new MemoryStream();
            using (var bodyReader = new StreamReader(context.Request.Body))
            {
                var bodyAsText = bodyReader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(bodyAsText) == false)
                {
                    requestLog += $", Body : {bodyAsText}";
                }

                var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                injectedRequestStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = injectedRequestStream;
            }
            _logger.LogDebug(AvlLogEvent.AvlRequest, 0, requestLog);
            await _next.Invoke(context);
        }
    }
}
