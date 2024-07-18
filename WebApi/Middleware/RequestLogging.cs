using System.Diagnostics;
using Serilog;

namespace WebApi.Middleware
{
    public class RequestLogging
    {
        private readonly RequestDelegate _next;

        public RequestLogging(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
                stopwatch.Stop();
                var logTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

                Log.Information(logTemplate,
                                context.Request.Method,
                                context.Request.Path,
                                context.Response.StatusCode,
                                stopwatch.Elapsed.TotalMilliseconds);
            }
            catch(Exception ex)
            {
                var logTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} Error \n{errorMessage}\n";

                Log.Information(logTemplate,
                                context.Request.Method,
                                context.Request.Path,
                                context.Response.StatusCode,
                                ex);

                throw new Exception(ex.Message);
            }
        }
    }
}