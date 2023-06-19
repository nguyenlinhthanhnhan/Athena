using System.Globalization;
using System.Runtime.Serialization;

namespace Athena.Application.Commons.Exceptions;

[Serializable]
public class UnauthorizedException : Exception
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture,
        message,
        args))
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}