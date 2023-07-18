using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.CustomMiddlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

         public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

     private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;

        var errorResponse = new ServiceResponse<HttpContext>
        {
            Success = false
        };

      String message = exception.Message;


                if (exception.Message.Contains("Invalid Token"))
                {
                    response.StatusCode = (int) HttpStatusCode.Forbidden;
                    errorResponse.Message = exception.Message;

                } else if (exception.Message.Contains(" SQL "))
                 {
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Problem connecting to the database.";
                } else {
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal server error.";
               }
               
        _logger.LogError(errorResponse.Message);
        await response.WriteAsJsonAsync(errorResponse);
    }

    }
}