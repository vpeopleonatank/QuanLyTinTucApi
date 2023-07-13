using System.Runtime.Serialization;

namespace HD.Station.QuanLyTinTuc.Abstractions.Exceptions;

[Serializable]
public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Not authenticated")
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
