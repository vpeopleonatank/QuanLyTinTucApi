using HD.Station.QuanLyTinTuc.Abstractions.Data;

using FluentValidation;
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

public class NewCommentDto
{

    public required string Body { get; set; }
}

public class CommentCreateValidator : AbstractValidator<NewCommentQuery>
{
    public CommentCreateValidator()
    {
        RuleFor(x => x.Comment.Body).NotNull().NotEmpty();
    }
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
public record SingleCommentResponse(CommentDTO Comment);

public record CommentsListQuery(string Slug);
public record NewCommentQuery(string Slug, NewCommentDto Comment);
public record NewCommentRequest(NewCommentDto Comment);

public record DeleteCommentQuery(string Slug, int Id);
