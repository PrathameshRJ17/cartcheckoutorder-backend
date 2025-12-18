namespace FoodOrderingSystem.API.DTOs.Common
{
    public class ErrorResponseDto
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
        public Dictionary<string, string[]>? ValidationErrors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string TraceId { get; set; } = string.Empty;
    }
}