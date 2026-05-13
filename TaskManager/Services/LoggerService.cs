namespace TaskManager.Services
{
    public class LoggerService : ILoggerService
    {
        private Guid _id = Guid.NewGuid();

        public LoggerService()
        {
            Console.WriteLine("LoggerService Created");
        }
        public Guid GetId() => _id;


    }
}
