using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Exceptions.UserExceptions;

public class TermsAndPrivacyPolicyException : Exception, IBaseException
{
    public int Code => StatusCodes.Status412PreconditionFailed;

    public string ErrorMessage { get; }

    public TermsAndPrivacyPolicyException() : base("Please accept Terms And Privacy Policy!")
    {
        ErrorMessage = "Please accept Terms And Privacy Policy!";
    }

    public TermsAndPrivacyPolicyException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}