
namespace HD.Station.QuanLyTinTuc.Abstractions.DTO;

public class LoginUserDto
{
    public required string Username { get; set; }

    public required string Password { get; set; }
}

public record LoginUserRequest(LoginUserDto User);
