using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Abstractions.Data;

public partial class Comment : IAuditableEntity
{
    public int CommentId { get; set; }

    public int? ArticleId { get; set; }
    public virtual required Article Article { get; set; }

    public int? UserId { get; set; }

    public virtual required User Author { get; set; }

    public required string CommentBody { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
