using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Abstractions.Data;

public partial class Article : IAuditableEntity
{
    public int ArticleId { get; set; }

    public int? UserId { get; set; }

    public int? TopicId { get; set; }

    public string? BannerImage { get; set; }

    public string? Slug { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Body { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual IReadOnlyCollection<ArticleTag> Tags => _tags;

    public virtual User? Author { get; set; }

    private readonly List<Comment> _comments = new();

    private readonly List<ArticleTag> _tags = new();

    public void AddTags(IEnumerable<Tag> existingTags, params string[] newTags)
    {
        _tags.AddRange(
            newTags
                .Where(x => !String.IsNullOrEmpty(x))
                .Distinct()
                .Select(x =>
                {
                    var tag = existingTags.FirstOrDefault(t => t.TagName == x);

                    return new ArticleTag
                    {
                        Tag = tag ?? new Tag { TagName = x },
                        Article = this
                    };
                })
                .ToList()
        );
    }
}
