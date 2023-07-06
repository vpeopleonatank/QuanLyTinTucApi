
namespace HD.Station.QuanLyTinTuc.Abstractions.Data;

public partial class Article
{
    public int ArticleId { get; set; }

    public int? UserId { get; set; }

    public int? TopicId { get; set; }

    public string? BannerImage { get; set; }

    public string? Slug { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Body { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ArticleTag> Tags { get; set; } = new List<ArticleTag>();

    public virtual User? Author { get; set; }
}
