using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HD.Station.QuanLyTinTuc.Mvc.ApiController;

[ApiController]
[Route("api/article/{slug}/[controller]")]

public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;
    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    // [HttpGet("{slug}", Name = "GetArticleComments")]
    // public async Task<MultipleCommentsResponse> List(string slug)
    // {
    //     
    // }
}
