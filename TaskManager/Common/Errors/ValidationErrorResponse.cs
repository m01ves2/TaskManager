namespace TaskManager.Common.Errors
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
