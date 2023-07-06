using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Helpers;

namespace HD.Station.QuanLyTinTuc.Abstractions.Stores;

public interface INewsStore
{
  public Task<PagedResponse<Article>> GetArticles(ArticlesListQuery articlesListQuery);
}
