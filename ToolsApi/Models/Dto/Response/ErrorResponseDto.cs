namespace ToolsApi.Models.Dto.Response
{
    public class ErrorResponseDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public DateTime ErrorDate { get; set; } = DateTime.UtcNow;
    }
}
