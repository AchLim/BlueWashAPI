namespace WebAPI.Exception
{
    public class DatabaseUpdateException : System.Exception
    {
        public DatabaseUpdateException(string? message, System.Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
