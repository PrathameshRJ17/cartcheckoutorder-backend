using FoodOrderingSystem.API.DTOs.Common;
using FoodOrderingSystem.API.Exceptions.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace FoodOrderingSystem.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponseDto
            {
                TraceId = context.TraceIdentifier,
                Timestamp = DateTime.UtcNow
            };

            switch (exception)
            {
                case ValidationException validationEx:
                    response.StatusCode = (int)validationEx.StatusCode;
                    errorResponse.ErrorCode = validationEx.ErrorCode;
                    errorResponse.Message = validationEx.Message;
                    errorResponse.ValidationErrors = validationEx.Errors;
                    break;

                case BaseCustomException customEx:
                    response.StatusCode = (int)customEx.StatusCode;
                    errorResponse.ErrorCode = customEx.ErrorCode;
                    errorResponse.Message = customEx.Message;
                    break;

                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.ErrorCode = "UNAUTHORIZED";
                    errorResponse.Message = "Access denied";
                    break;

                case ArgumentException argEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.ErrorCode = "INVALID_ARGUMENT";
                    errorResponse.Message = argEx.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.ErrorCode = "INTERNAL_SERVER_ERROR";
                    errorResponse.Message = "An unexpected error occurred";
                    errorResponse.Details = exception.Message;
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(jsonResponse);
        }
    }
}