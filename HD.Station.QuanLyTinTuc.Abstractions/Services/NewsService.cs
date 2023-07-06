using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Abstractions.Services;

public class NewsService : INewsService
{
    private readonly INewsStore _newsStore;
    public NewsService(INewsStore newsStore)
    {
      _newsStore = newsStore;
    }
    public async Task<MultipleArticlesResponse> GetArticles(ArticlesListQuery articlesListQuery)
    {
      var articles = await _newsStore.GetArticles(articlesListQuery);
      var articlesDto = articles.Items.Select(a => a.Map());

      return new MultipleArticlesResponse(articlesDto, articles.Total);
    }
}
