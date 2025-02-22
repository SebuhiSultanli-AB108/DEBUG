using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Exceptions.UserExceptions;

public class CantFollowSelfException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public CantFollowSelfException() : base("Can't follow self!")
    {
        ErrorMessage = "Can't follow self!";
    }

    public CantFollowSelfException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}