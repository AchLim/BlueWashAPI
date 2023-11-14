namespace WebAPI.Exception
{
    public class UnauthorizedAccessException : System.Exception
    {
        public UnauthorizedAccessException(string? message, System.Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
