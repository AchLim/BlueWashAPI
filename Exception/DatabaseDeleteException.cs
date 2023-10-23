namespace WebAPI.Exception
{
    public class DatabaseDeleteException : System.Exception
    {
        public DatabaseDeleteException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}
