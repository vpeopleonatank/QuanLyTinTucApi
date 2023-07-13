using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HD.Station.QuanLyTinTuc.Mvc.ApiController;

[ApiController]
[Route("api/[controller]")]
// [ApiExplorerSettings(GroupName = "Articles")]
public class ArticlesController : ControllerBase
{
    private readonly INewsService _newService;
    public ArticlesController(INewsService newsService)
    {
        _newService = newsService;
    }

    [HttpGet(Name = "GetArticles")]
    public async Task<ActionResult<MultipleArticlesResponse>> GetArticles([FromQuery] ArticlesListQuery query)
    {
        var articles = await _newService.GetArticles(query);

        return articles;
    }

    [HttpGet("{slug}", Name = "GetArticle")]
    public async Task<ActionResult<SingleArticleResponse>> GetArticle(string slug)
    {
        var article = await _newService.GetArticle(new ArticleGetQuery(slug));

        return article;
    }

    [Authorize(Roles = "Admin,Writer")]
    [HttpPost(Name = "CreateArticle")]
    public async Task<ActionResult<SingleArticleResponse>> CreateArticle([FromBody] NewArticleRequest request)
    {
        var article = await _newService.CreateArticle(request);

        return article;
    }

    [Authorize(Roles = "Admin,Writer")]
    [HttpPut(Name = "UpdateArticle")]
    public async Task<ActionResult<SingleArticleResponse>> UpdateArticle([FromBody] UpdateArticleRequest request)
    {
        var article = await _newService.UpdateArticle(request);

        return article;
    }

    [Authorize(Roles = "Admin,Writer")]
    [HttpDelete("{slug}", Name = "DeleteArticle")]
    public async Task DeleteArticle(string slug)
    {
        await _newService.RemoveArticle(new DeleteArticleRequest(slug));
    }

}
