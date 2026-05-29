namespace TaskManager.Errors
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
