namespace WebAPI.Exception
{
    public class DatabaseUpdateException : System.Exception
    {
        public DatabaseUpdateException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}
