
namespace HD.Station.QuanLyTinTuc.Abstractions.DTO;

public class UpdateUserDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
}

public record UpdateUserRequest(UpdateUserDto User);

