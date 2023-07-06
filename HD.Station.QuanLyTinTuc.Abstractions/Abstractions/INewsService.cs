using HD.Station.QuanLyTinTuc.Abstractions.DTO;

namespace HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

public interface INewsService
{
    public Task<MultipleArticlesResponse> GetArticles(ArticlesListQuery articlesListQuery);
}
