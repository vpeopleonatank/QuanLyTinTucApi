using HD.Station.QuanLyTinTuc.Abstractions.DTO;

namespace HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

public interface INewsService
{
    public Task<MultipleArticlesResponse> GetArticles(ArticlesListQuery articlesListQuery);
    public Task<SingleArticleResponse> GetArticle(ArticleGetQuery articleGetQuery);
    public Task<UserResponse> CreateNewUser(NewUserRequest newUserRequest);
    public Task<UserResponse> LoginUser(LoginUserRequest loginUserRequest);
    public Task<UserResponse> GetCurrentUser();
    public Task<SingleArticleResponse> CreateArticle(NewArticleRequest request);
    public Task<SingleArticleResponse> UpdateArticle(UpdateArticleRequest request);
    public Task RemoveArticle(DeleteArticleRequest request);
    public Task<TopicsResponse> GetTopics();
    public Task<NewTopicRequest> AddTopic(NewTopicRequest request);
    public Task<RemoveTopicResponse> RemoveTopic(string topicId);
}
