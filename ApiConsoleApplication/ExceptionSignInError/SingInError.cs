using System.IO.Enumeration;

namespace ApiConsoleApplication.ExceptionSignInError;

public class SingInError : ApplicationException
{
    public SingInError(string message) : base(message)
    {
    }

    
}