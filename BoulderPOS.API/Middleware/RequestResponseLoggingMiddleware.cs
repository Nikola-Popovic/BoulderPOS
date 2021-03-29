using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace BoulderPOS.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                .CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            
            var informationLog = new StringBuilder()
                .Append($"Http Request Information: {Environment.NewLine}")
                .Append($"From: {context.Request.Scheme}://{context.Request.Host} {Environment.NewLine}")
                .Append($"{context.Request.Method} ")
                .Append($"{context.Request.Path} Query :{context.Request.QueryString} {Environment.NewLine}");

            requestStream.Position = 0;
            using var reader = new StreamReader(requestStream);
            var requestBody = await reader.ReadToEndAsync();

            informationLog.Append($"Request body : {requestBody}");
            context.Request.Body.Position = 0;
            _logger.LogInformation(informationLog.ToString());
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBody = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            // Execute the next middleware on the pipeline
            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);


            var informationLog = new StringBuilder()
                .Append($"Http Response Information: {Environment.NewLine}")
                .Append($"From: {context.Request.Scheme}://{context.Request.Host} {Environment.NewLine}")
                .Append($"{context.Request.Method} {context.Response.StatusCode} ")
                .Append($"{context.Request.Path} Query :{context.Request.QueryString} {Environment.NewLine}")
                .Append($"Response Body : {responseString}");
            _logger.LogInformation(informationLog.ToString());

            await responseBody.CopyToAsync(originalBody);
        }
    }
}
