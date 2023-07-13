using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HD.Station.QuanLyTinTuc.Mvc.ApiController;

[ApiController]
[Route("api/articles/{slug}/[controller]")]
// [ApiExplorerSettings(GroupName = "Comments")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;
    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [AllowAnonymous]
    [HttpGet(Name = "GetArticleComments")]
    public async Task<MultipleCommentsResponse> List(string slug)
    {
      var comments = await _commentsService.GetComments(new CommentsListQuery(slug));

      return comments;
    }

    [HttpPost(Name = "CreateArticleComment")]
    public async Task<SingleCommentResponse> Create(string slug, [FromBody] NewCommentRequest request)
    {
      var comment = await _commentsService.AddNewComment(new NewCommentQuery(slug, request.Comment));

      return comment;
    }

    [HttpDelete("{commentId}", Name = "DeleteComment")]
    public async Task Delete(string slug, int commentId)
    {
      await _commentsService.DeleteComment(new DeleteCommentQuery(slug, commentId));
    }
}
