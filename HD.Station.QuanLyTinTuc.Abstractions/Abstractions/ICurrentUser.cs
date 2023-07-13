using HD.Station.QuanLyTinTuc.Abstractions.Data;

namespace HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

public interface ICurrentUser
{
    User? User { get; }

    Task SetIdentifier(long identifier);
}
