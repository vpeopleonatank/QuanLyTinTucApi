using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

using FluentValidation;

using HD.Station.QuanLyTinTuc.Abstractions.Exceptions;
using ValidationException = HD.Station.QuanLyTinTuc.Abstractions.Exceptions.ValidationException;

namespace HD.Station.QuanLyTinTuc.Abstractions.Services;

public class CommentsService : ICommentsService
{
    private readonly INewsStore _newsStore;
    private readonly ICurrentUser _currentUser;
    private readonly IValidator<NewCommentQuery> _newCommentValidator;
    public CommentsService(INewsStore newsStore,
        ICurrentUser currentUser,
        IValidator<NewCommentQuery> newCommentQuery)
    {
        _newsStore = newsStore;
        _currentUser = currentUser;
        _newCommentValidator = newCommentQuery;
    }

    public async Task<MultipleCommentsResponse> GetComments(CommentsListQuery request)
    {
        var article = await _newsStore.FindArticleBySlug(request.Slug);

        var comments = (await _newsStore.GetCommentsByArticleId(article.ArticleId))

            .Select(c => c.Map(_currentUser.User));

        return new MultipleCommentsResponse(comments);
    }

    public async Task<SingleCommentResponse> AddNewComment(NewCommentQuery request)
    {
        var context = new ValidationContext<NewCommentQuery>(request);
        var validationResult = await _newCommentValidator.ValidateAsync(context);
        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }

        var commentDTO = await _newsStore.AddCommentToArticle(request, _currentUser.User!);

        return new SingleCommentResponse(commentDTO);
    }

    public async Task DeleteComment(DeleteCommentQuery request)
    {
        var article = await _newsStore.FindArticleBySlug(request.Slug);
        var comment = await _newsStore.GetCommentByArticle(request.Id, article.ArticleId);

        if (article.UserId != _currentUser.User!.Id && comment.UserId != _currentUser.User!.Id)
        {
            throw new ForbiddenException();
        }

        await _newsStore.RemoveComment(comment);
    }
}
