using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;

using Microsoft.AspNetCore.Mvc;

namespace HD.Station.QuanLyTinTuc.Mvc.ApiController;

[ApiController]
[Route("api/[controller]")]

public class QlttController : ControllerBase
{
    private readonly INewsService _newService;
    public QlttController(INewsService newsService)
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
    public async Task<ActionResult<SingleArticleResponse>> GetArticle([FromQuery] string slug)
    {
        var article = await _newService.GetArticle(new ArticleGetQuery(slug));

        return article;
    }

    // [HttpPost(Name = "CreateArticle")]
    // public async Task<ActionResult<SingleArticleResponse>> CreateArticle([FromBody] NewArticleRequest request)
    // {

    // }
}
