using Microsoft.EntityFrameworkCore;

using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Helpers;

namespace HD.Station.QuanLyTinTuc.SqlServer.Stores;

public class NewsStore : INewsStore
{
    private readonly QuanLyTinTucDbContext _qlttDbContext;
    private readonly IPasswordHasher _passwordHasher;
    public NewsStore(QuanLyTinTucDbContext quanLyTinTucDbContext, IPasswordHasher passwordHasher)
    {
        _qlttDbContext = quanLyTinTucDbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<PagedResponse<Article>> GetArticles(ArticlesListQuery articlesListQuery)
    {
        var articles = await _qlttDbContext.Articles
            .Include(a => a.Author)
            .Include(a => a.Tags)
            .ThenInclude(t => t.Tag)
            .FilterByAuthor(articlesListQuery.Author)
            .FilterByTag(articlesListQuery.Tag)
            .OrderByDescending(x => x.UserId)
            .PaginateAsync(articlesListQuery);

        return articles;
    }

    public async Task<Article> GetArticle(string slug)
    {
        var article = await _qlttDbContext.Articles
            .FindAsync(x => x.Slug == slug);

        return article;
    }

    // public async Task<ArticleDto> GetArticle(NewArticleRequest request)
    // {
    //     var article = new Article
    //     {
    //         Title = request.Article.Title,
    //         Description = request.Article.Description,
    //         Body = request.Article.Body,
    //         Author = _currentUser.User!,
    //         Slug = _slugifier.Generate(request.Article.Title)
    //     }
    // }
    //

    public async Task<bool> isUserExists(string username)
    {
        return await _qlttDbContext.Users
              .Where(x => x.Username == username)
              .AnyAsync();

    }

    public async Task<bool> isSlugExists(string slug)
    {
        return await _qlttDbContext.Articles
                  .Where(x => x.Slug == slug)
                  .AnyAsync();
    }

    public async Task<Article> CreateNewArticle(Article article)
    {
        await _qlttDbContext.Articles.AddAsync(article);
        await _qlttDbContext.SaveChangesAsync();

        return article;
    }

    public async Task<List<Tag>> GetExistingTags(ICollection<string> tagList)
    {
        var existingTags = await _qlttDbContext.Tags
            .Where(
                x => tagList
                    .AsEnumerable()
                    .Any(t => t == x.TagName)
            )
            .ToListAsync();

        return existingTags;
    }

    public async Task<User> CreateNewUser(NewUserRequest newUserRequest)
    {
        var user = new User
        {
            Username = newUserRequest.User.Username,
            Email = newUserRequest.User.Email,
            Password = _passwordHasher.Hash(newUserRequest.User.Password),
            Role = newUserRequest.User.Role
        };
        await _qlttDbContext.AddAsync(user);
        await _qlttDbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        var user = await _qlttDbContext.Users.Where(x => x.Username == username)
          .SingleOrDefaultAsync();

        return user;
    }

    public async Task<User> GetUserById(long identifier)
    {
        return await _qlttDbContext.Users
          .Where(x => x.Id == identifier).SingleOrDefaultAsync();
    }

    public async Task<List<Topic>> GetTopics()
    {
        return await _qlttDbContext.Topics.ToListAsync();
    }

    public async Task<Topic> AddTopic(string topicName)
    {
        var topic = new Topic
        {
            TopicName = topicName
        };
        await _qlttDbContext.Topics.AddAsync(topic);
        await _qlttDbContext.SaveChangesAsync();

        return topic;
    }

    public async Task<Topic?> RemoveTopic(int topicId)
    {
        var topic = await _qlttDbContext.Topics
          .Where(x => x.TopicId == topicId)
          .FirstOrDefaultAsync();
        if (topic == null)
            return null;
        _qlttDbContext.Topics.Remove(topic);
        await _qlttDbContext.SaveChangesAsync();

        return topic;
    }

    public async Task<Topic?> FindTopicById(int topicId)
    {
        var topic = await _qlttDbContext.Topics.FindAsync(x => x.TopicId == topicId);

        return topic;
    }

    public async Task UpdateTopic(Topic topic)
    {
        _qlttDbContext.Topics.Update(topic);
        await _qlttDbContext.SaveChangesAsync();
    }

    public async Task<bool> isTopicNameExists(string topicName)
    {
        return await _qlttDbContext.Topics
          .Where(x => x.TopicName == topicName)
          .AnyAsync();
    }

    public async Task<Article> FindArticleBySlug(string slug)
    {
        return await _qlttDbContext.Articles
          .Include(x => x.Tags)
          .ThenInclude(x => x.Tag)  // Tag field in Tags
          .FindAsync(x => x.Slug == slug);
    }

    public async Task UpdateNewArticle(Article article)
    {
        _qlttDbContext.Articles.Update(article);
        await _qlttDbContext.SaveChangesAsync();
    }

    public async Task RemoveArticle(Article article)
    {
        _qlttDbContext.Articles.Remove(article);
        await _qlttDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>> GetCommentsByArticleId(int articleId)
    {
        var comments = await _qlttDbContext.Comments
            .Include(c => c.Author)
            .Where(c => c.ArticleId == articleId)
            .OrderByDescending(x => x.CommentId)
            // .Select(c => c.Map(_currentUser.User))
            .ToListAsync();

        return comments;
    }

    public async Task<CommentDTO> AddCommentToArticle(NewCommentQuery query, User user)
    {
        var article = await FindArticleBySlug(query.Slug);
        var comment = new Comment
        {
            CommentBody = query.Comment.Body,
            Article = article,
            Author = user
        };

        await _qlttDbContext.Comments.AddAsync(comment);
        await _qlttDbContext.SaveChangesAsync();

        return comment.Map(user);
    }


    public async Task<Comment> GetCommentByArticle(int commentId, int articleId)
    {
        return await _qlttDbContext.Comments.FindAsync(
            x => x.CommentId == commentId && x.ArticleId == articleId
            );
    }

    public async Task RemoveComment(Comment comment)
    {
        _qlttDbContext.Comments.Remove(comment);
        await _qlttDbContext.SaveChangesAsync();
    }
}
