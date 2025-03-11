using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Exceptions;

public class PageOrTakeCantBeZeroException : Exception, IBaseException
{
    public int Code => StatusCodes.Status412PreconditionFailed;

    public string ErrorMessage { get; }

    public PageOrTakeCantBeZeroException() : base("PageNo or Take cant be less than zero!")
    {
        ErrorMessage = "PageNo or Take cant be less than zero!";
    }

    public PageOrTakeCantBeZeroException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}