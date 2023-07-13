
namespace HD.Station.QuanLyTinTuc.Abstractions.Data;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? ArticleId { get; set; }

    public int? UserId { get; set; }

    public required string CommentBody { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual required User Author { get; set; }
}
