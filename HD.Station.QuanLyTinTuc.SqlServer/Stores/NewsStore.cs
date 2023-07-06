using Microsoft.EntityFrameworkCore;

using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Helpers;

namespace HD.Station.QuanLyTinTuc.SqlServer.Stores;

public class NewsStore : INewsStore
{
    private readonly QuanLyTinTucDbContext _qlttDbContext;
    public NewsStore(QuanLyTinTucDbContext quanLyTinTucDbContext)
    {
        _qlttDbContext = quanLyTinTucDbContext;
    }

    public async Task<PagedResponse<Article>> GetArticles(ArticlesListQuery articlesListQuery)
    {
        var articles = await _qlttDbContext.Articles
            .Include(a => a.Author)
            .Include(a => a.Tags)
            .ThenInclude(t => t.Tag)
            .FilterByAuthor(articlesListQuery.Author)
            .FilterByTag(articlesListQuery.Tag)
            .OrderByDescending(x => x.UserId)
            .PaginateAsync(articlesListQuery);

        return articles;
    }

    public async Task<Article> GetArticle(string slug)
    {
        var article = await _qlttDbContext.Articles
            .FindAsync(x => x.Slug == slug);

        return article;
    }

}
