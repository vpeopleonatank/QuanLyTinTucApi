using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Abstractions.Services;

public class CommentsService : ICommentsService
{
    private readonly INewsStore _newsStore;
    private readonly ICurrentUser _currentUser;
    public CommentsService(INewsStore newsStore, ICurrentUser currentUser)
    {
        _newsStore = newsStore;
        _currentUser = currentUser;
    }

    public async Task<MultipleCommentsResponse> GetComments(CommentsListQuery request)
    {
        var article = await _newsStore.FindArticleBySlug(request.Slug);

        var comments = (await _newsStore.GetCommentsByArticleId(article.ArticleId))

            .Select(c => c.Map(_currentUser.User));

        return new MultipleCommentsResponse(comments);
    }
}
