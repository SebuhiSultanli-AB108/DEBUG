using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Exceptions.UserExceptions;

public class AlreadyFollowingException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public AlreadyFollowingException() : base("This User already been followed!")
    {
        ErrorMessage = "This User already been followed!";
    }

    public AlreadyFollowingException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
