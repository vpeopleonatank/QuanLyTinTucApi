using System.Runtime.Serialization;

namespace HD.Station.QuanLyTinTuc.Abstractions.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Access forbidden")
    {
    }

    protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
