namespace TaskManager.Common.Errors
{
    public static class ErrorCodes
    {
        public const string TaskAlreadyExists = "TASK_ALREADY_EXISTS";
        public const string TaskDoesNotExist = "TASK_DOES_NOT_EXIST";
        public const string TaskTitleInvalid = "TASK_TITLE_INVALID";

        public const string ValidationError = "VALIDATION_ERROR";
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
    }
}
