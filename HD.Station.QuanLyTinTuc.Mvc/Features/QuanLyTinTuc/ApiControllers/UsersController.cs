using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Mvc.ApiController;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "User and Authentication")]
public class UsersController : Controller
{
    private readonly INewsService _newsService;
    private readonly IValidator<NewUserRequest> _newUserValidator;
    public UsersController(INewsService newsService, IValidator<NewUserRequest> newUserRequest)
    {
        _newsService = newsService;
        _newUserValidator = newUserRequest;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register([FromBody] NewUserRequest newUserRequest, CancellationToken cancellationToken)
    {
        var userResponse = await _newsService.CreateNewUser(newUserRequest);
        return userResponse;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login([FromBody] LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
    {
        var userResponse = await _newsService.LoginUser(loginUserRequest);
        return userResponse;
    }

    [HttpPost("create_admin")]
    public async Task<ActionResult<UserResponse>> CreateAdmin([FromBody] NewUserRequest newUserRequest, CancellationToken cancellationToken)
    {
        newUserRequest.User.Role = "Admin";
        var userResponse = await _newsService.CreateNewUser(newUserRequest);
        return userResponse;
    }
}
