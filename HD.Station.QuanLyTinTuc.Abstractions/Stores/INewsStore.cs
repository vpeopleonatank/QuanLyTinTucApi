using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Helpers;

namespace HD.Station.QuanLyTinTuc.Abstractions.Stores;

public interface INewsStore
{
    public Task<PagedResponse<Article>> GetArticles(ArticlesListQuery articlesListQuery);
    public Task<Article> GetArticle(string slug);
    public Task<bool> isUserExists(string username);
    public Task<User> CreateNewUser(NewUserRequest newUserRequest);
    public Task<User> GetUserByUsername(string username);
    public Task<User> GetUserById(long identifier);
    public Task<bool> isSlugExists(string slug);
    public Task<Article> CreateNewArticle(Article article);
    public Task UpdateNewArticle(Article article);
    public Task RemoveArticle(Article article);
    public Task<Article> FindArticleBySlug(string slug);
    public Task<List<Tag>> GetExistingTags(ICollection<string> tagList);
    public Task<List<Topic>> GetTopics();
    public Task<Topic> AddTopic(string topicName);
    public Task<Topic?> RemoveTopic(int topicId);
    public Task<bool> isTopicNameExists(string topicName);
    public Task<IEnumerable<Comment>> GetCommentsByArticleId(int articleId);
  // public Task<ArticleDto> GetArticle(NewArticleRequest newArticleRequest);
    public Task<CommentDTO> AddCommentToArticle(NewCommentQuery query, User user);
    public Task<Comment> GetCommentByArticle(int commentId, int articleId);
    public Task RemoveComment(Comment comment);
    public Task<Topic?> FindTopicById(int topicId);
    public Task UpdateTopic(Topic topic);
}
