using HD.Station.QuanLyTinTuc.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace HD.Station.QuanLyTinTuc.Abstractions.DTO;

public class CommentDTO
{
    public int Id { get; set; }

    public required string Body { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public required ProfileDto Author { get; set; }
}

public static class CommentMapper
{
    public static CommentDTO Map(this Comment comment, User? user)
    {
        return new()
        {
            Id = comment.CommentId,
            Body = comment.CommentBody,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Author = comment.Author.MapToProfile(user),
        };
    }
}

public record MultipleCommentsResponse(IEnumerable<CommentDTO> Comments);

public record CommentsListQuery(string Slug);
