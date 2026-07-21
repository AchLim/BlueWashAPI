namespace WebAPI.Exception
{
    public class DatabaseUniqueConstraintException : System.Exception
    {
        public DatabaseUniqueConstraintException(string? message, System.Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
