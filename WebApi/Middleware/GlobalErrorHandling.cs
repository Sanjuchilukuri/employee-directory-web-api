
using Microsoft.EntityFrameworkCore;

namespace WebApi.Middleware
{
    public class GlobalErrorHandling
    {
        private RequestDelegate _next;

        public GlobalErrorHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch (ex)
                {
                    case DbUpdateException:
                        response.StatusCode = (int)StatusCodes.Status409Conflict;
                        break;
                    case ArgumentNullException:
                        response.StatusCode = (int)StatusCodes.Status400BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                        break;
                }
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
        }
    }
}