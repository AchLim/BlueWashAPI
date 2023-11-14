namespace WebAPI.Exception
{
    public class InvalidDataException : System.Exception
    {
        public InvalidDataException(string? message, System.Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
