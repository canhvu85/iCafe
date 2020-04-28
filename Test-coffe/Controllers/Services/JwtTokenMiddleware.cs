using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITokenBuilder tokenBuilder)
        {
            if (httpContext.Request.Path == "/api/CitiesAPI" ||
               httpContext.Request.Path.StartsWithSegments("/api/ShopsAPI") ||
               httpContext.Request.Path.StartsWithSegments("/uploads") ||
                !httpContext.Request.Path.StartsWithSegments("/api"))
            {
                await _next(httpContext);
            }
            else
            {
                var isExpired = tokenBuilder.isExpiredToken();
                if (isExpired == false)
                {
                    await _next(httpContext);
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await httpContext.Response.WriteAsync("Access denied !");
                }
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtTokenMiddleware>();
        }
    }
}
