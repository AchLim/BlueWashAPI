namespace WebAPI.Exception
{
    public class DatabaseReadException : System.Exception
    {
        public DatabaseReadException(string? message, System.Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
