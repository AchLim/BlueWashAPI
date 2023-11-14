namespace WebAPI.Exception
{
    public class UserNotFoundException : System.Exception
    {
        public UserNotFoundException(string? message, System.Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
