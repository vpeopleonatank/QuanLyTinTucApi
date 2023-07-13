using HD.Station.QuanLyTinTuc.Abstractions.Data;

namespace HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
public interface IJwtTokenGenerator
{
    string CreateToken(User user);
}
