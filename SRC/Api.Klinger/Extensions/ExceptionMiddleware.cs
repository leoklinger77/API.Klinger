using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Elmah.Io.AspNetCore;

namespace Api.Klinger.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                e.Ship(httpContext);               
            }
        }       
    }
}
