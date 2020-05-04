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
        private string _token;

        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITokenBuilder tokenBuilder)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api"))
            {
                if (httpContext.Request.Path == "/api/CitiesAPI" ||
                     httpContext.Request.Path == "/api/mobile/CitiesAPI" ||
                     httpContext.Request.Path.StartsWithSegments("/api/mobile/ShopsApi") ||
                     httpContext.Request.Path.StartsWithSegments("/api/mobile/UsersApi") ||
                     httpContext.Request.Path.StartsWithSegments("/api/ShopsAPI"))
                {
                    await _next(httpContext);
                }
                else
                {
                    string remember_token = httpContext.Request.Headers["Authorization"];
                    _token = remember_token;
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
            else
            {
                await _next(httpContext);
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
