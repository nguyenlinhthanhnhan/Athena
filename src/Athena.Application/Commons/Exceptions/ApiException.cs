using System.Globalization;
using System.Runtime.Serialization;

namespace Athena.Application.Commons.Exceptions;

[Serializable]
public class ApiException : Exception
{
    public ApiException()
    {
    }

    public ApiException(string message) : base(message)
    {
    }

    public ApiException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message,
        args))
    {
    }

    protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}