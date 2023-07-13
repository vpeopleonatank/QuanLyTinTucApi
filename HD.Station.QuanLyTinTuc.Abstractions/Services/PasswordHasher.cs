using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Abstractions.Services;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Check(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

}
