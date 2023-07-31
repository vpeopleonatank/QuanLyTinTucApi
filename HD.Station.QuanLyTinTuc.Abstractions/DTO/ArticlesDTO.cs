using System.Collections.ObjectModel;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using HD.Station.QuanLyTinTuc.Abstractions.Helpers;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;

namespace HD.Station.QuanLyTinTuc.Abstractions.DTO;

public class ProfileDto
{
    public required string Username { get; set; }

    public string? Bio { get; set; }

    public string? Image { get; set; }
}

public class ArticleDto
{
    public required string Title { get; set; }

    public string? BannerImage { get; set; }

    public required string Slug { get; set; }

    public required string Description { get; set; }

    public required string Body { get; set; }

    public Collection<string> TagList { get; set; } = new();

    // public required ProfileDto Author { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}

public class NewArticleDto
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Body { get; set; }

    public required int TopicId { get; set; }

    public required IFormFile BannerImage { get; set; }

    public Collection<string> TagList { get; set; } = new();
}

public class UpdateArticleDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Body { get; set; }
    public IFormFile? BannerImage { get; set; }
}

public class ArticleCreateValidator : AbstractValidator<NewArticleRequest>
{
    public ArticleCreateValidator(INewsStore newsStore,
        ISlugifier slugifier)
    {
        RuleFor(x => x.Article.Title).NotNull().NotEmpty();
        RuleFor(x => x.Article.Description).NotNull().NotEmpty();
        RuleFor(x => x.Article.Body).NotNull().NotEmpty();
        RuleFor(x => x.Article.BannerImage).NotNull().NotEmpty();

        RuleFor(x => x.Article.Title).MustAsync(
            async (title, cancellationToken) => 
            !await newsStore.isSlugExists(slugifier.Generate(title!))
        )
            .WithMessage("Slug with this title already used");
    }
}

public class ArticleUpdateValidator : AbstractValidator<UpdateArticleRequest>
{
    public ArticleUpdateValidator()
    {
        RuleFor(x => x.Article.Title).NotEmpty().When(x => x.Article.Title != null);
        RuleFor(x => x.Article.Description).NotEmpty().When(x => x.Article.Description != null);
        RuleFor(x => x.Article.Body).NotEmpty().When(x => x.Article.Body != null);
        RuleFor(x => x.Article.BannerImage).NotEmpty().When(x => x.Article.BannerImage != null);
    }
}

public record NewArticleRequest(NewArticleDto Article);
public record UpdateArticleRequest(string Slug, UpdateArticleDto Article);
public record DeleteArticleRequest(string Slug);

public record SingleArticleResponse(ArticleDto Article);
public record MultipleArticlesResponse(IEnumerable<ArticleDto> Articles, int ArticlesCount);

public record ArticleGetQuery(string Slug);

public class ArticlesListQuery : PagedQuery
{
    public string? Author { get; set; }

    public string? Tag { get; set; }

    public string? TopicId { get; set; }
}

public static class ArticleMapper
{
    public static ArticleDto Map(this Article article) {
        return new()
        {
            Slug = article.Slug,
            Title = article.Title,
            BannerImage = "Images/" + article.BannerImage,
            Description = article.Description,
            Body = article.Body,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
            TagList = new Collection<string>(article.Tags.Select(t => t.Tag.TagName)
                .OrderBy(t => t).ToList())
        };
    }

    public static ProfileDto MapToProfile(this User user, User? currentUser)
    {
        return new()
        {
            Username = user.Username,
            Image = user.Image,
        };
    }
}
