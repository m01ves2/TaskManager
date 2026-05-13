namespace TaskManager.Services
{
    public class LoggerService : ILoggerService
    {
        private Guid _id = Guid.NewGuid();

        public Guid GetId() => _id;
    }
}
