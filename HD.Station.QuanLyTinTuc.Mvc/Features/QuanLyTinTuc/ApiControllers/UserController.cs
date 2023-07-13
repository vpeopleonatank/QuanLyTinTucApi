using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Mvc.ApiController;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "User and Authentication")]
public class UserController : Controller
{
    private readonly INewsService _newsService;
    public UserController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet]
    public async Task<ActionResult<UserResponse>> Current()
    {
        return await _newsService.GetCurrentUser();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create_writer")]
    public async Task<ActionResult<UserResponse>> CreateAdmin([FromBody] NewUserRequest newUserRequest, CancellationToken cancellationToken)
    {
        newUserRequest.User.Role = "Writer";
        var userResponse = await _newsService.CreateNewUser(newUserRequest);
        return userResponse;
    }


    [Authorize(Roles = "Admin,Writer")]
    [HttpGet("test_writer_role")]
    public ActionResult Test_writer_role()
    {
        return Ok("writer");
    }
}
