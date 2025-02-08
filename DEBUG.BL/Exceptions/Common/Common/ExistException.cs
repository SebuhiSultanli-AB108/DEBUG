using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Exceptions.Common;

public class ExistException<T> : Exception, IBaseException
{
    public int Code => StatusCodes.Status409Conflict;

    public string ErrorMessage { get; }

    public ExistException() : base(typeof(T).Name + " is exist!")
    {
        ErrorMessage = typeof(T).Name + " is exist!";
    }

    public ExistException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}