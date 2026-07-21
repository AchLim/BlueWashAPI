namespace WebAPI.Exception
{
    public class DatabaseInsertException : System.Exception
    {
        public DatabaseInsertException(string? message, System.Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
