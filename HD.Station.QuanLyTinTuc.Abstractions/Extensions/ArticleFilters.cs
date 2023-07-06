using HD.Station.QuanLyTinTuc.Abstractions.Data;


namespace HD.Station.QuanLyTinTuc.Abstractions.Helpers;

public static class ArticleFilters
{
    public static IQueryable<Article> FilterByAuthor(
        this IQueryable<Article> source,
        string? author
    )
    {
        if (string.IsNullOrEmpty(author))
        {
            return source;
        }
        return source.Where(a => a.Author.Username == author);
    }

    public static IQueryable<Article> FilterByTag(
        this IQueryable<Article> source,
        string? name
    )
    {
        if (!string.IsNullOrEmpty(name))
        {
            return source.Where(a => a.Tags.Any(t => t.Tag.TagName == name));
        }
        return source;
    }
}
