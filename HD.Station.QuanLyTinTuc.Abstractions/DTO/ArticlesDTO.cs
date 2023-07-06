using HD.Station.QuanLyTinTuc.Abstractions.Helpers;
using HD.Station.QuanLyTinTuc.Abstractions.Data;

namespace HD.Station.QuanLyTinTuc.Abstractions.DTO;

public class ArticleDto
{
    public required string Title { get; set; }

    public string? BannerImage { get; set; }

    public required string Slug { get; set; }

    public required string Description { get; set; }

    public required string Body { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}

public record MultipleArticlesResponse(IEnumerable<ArticleDto> Articles, int ArticlesCount);

public class ArticlesListQuery : PagedQuery
{
    public string? Author { get; set; }

    public string? Favorited { get; set; }

    public string? Tag { get; set; }

    public string? Topic { get; set; }
}

public static class ArticleMapper
{
    public static ArticleDto Map(this Article article)
    {
        return new()
        {
            Slug = article.Slug,
            Title = article.Title,
            Description = article.Description,
            Body = article.Body,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
        };
    }
}
