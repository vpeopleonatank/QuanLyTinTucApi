using FluentValidation;

using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

using ValidationException = HD.Station.QuanLyTinTuc.Abstractions.Exceptions.ValidationException;
using NotFoundException = HD.Station.QuanLyTinTuc.Abstractions.Exceptions.NotFoundException;
using ForbiddenException = HD.Station.QuanLyTinTuc.Abstractions.Exceptions.ForbiddenException;

namespace HD.Station.QuanLyTinTuc.Abstractions.Services;

public class NewsService : INewsService
{
    private readonly INewsStore _newsStore;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<NewUserRequest> _newUserValidator;
    private readonly IValidator<NewArticleRequest> _newArticleValidator;
    private readonly IValidator<NewTopicRequest> _newTopicValidator;
    private readonly IValidator<UpdateArticleRequest> _updateArticleValidator;
    private readonly ICurrentUser _currentUser;
    private readonly ISlugifier _slugifier;
    public NewsService(INewsStore newsStore, IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IValidator<NewUserRequest> newUserRequest,
        IValidator<NewArticleRequest> newArticleRequest,
        IValidator<NewTopicRequest> newTopicRequest,
        IValidator<UpdateArticleRequest> updateArticleRequest,
        ICurrentUser currentUser,
        ISlugifier slugifier)
    {
        _newsStore = newsStore;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _newUserValidator = newUserRequest;
        _newArticleValidator = newArticleRequest;
        _currentUser = currentUser;
        _slugifier = slugifier;
        _newTopicValidator = newTopicRequest;
        _updateArticleValidator = updateArticleRequest;
    }
    public async Task<MultipleArticlesResponse> GetArticles(ArticlesListQuery articlesListQuery)
    {
        var articles = await _newsStore.GetArticles(articlesListQuery);
        var articlesDto = articles.Items.Select(a => a.Map());

        return new MultipleArticlesResponse(articlesDto, articles.Total);
    }

    public async Task<SingleArticleResponse> GetArticle(ArticleGetQuery query)
    {
        var article = await _newsStore.FindArticleBySlug(query.Slug, true);

        return new SingleArticleResponse(article.Map());
    }


    public async Task<UserResponse> CreateNewUser(NewUserRequest newUserRequest)
    {
        var context = new ValidationContext<NewUserRequest>(newUserRequest);
        var validationResult = await _newUserValidator.ValidateAsync(context);
        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }
        var user = await _newsStore.CreateNewUser(newUserRequest);

        return new UserResponse(user.Map(_jwtTokenGenerator));
    }

    public async Task<UserResponse> LoginUser(LoginUserRequest loginUserRequest)
    {
        var user = await _newsStore.GetUserByUsername(loginUserRequest.User.Username);

        if (user?.Password is null || !_passwordHasher.Check(loginUserRequest.User.Password, user.Password))
        {
            throw new Exceptions.ValidationException("Bad credentials");
        }

        return new UserResponse(user.Map(_jwtTokenGenerator));
    }

    public async Task<UserResponse> GetCurrentUser()
    {
        return new UserResponse(
          await Task.FromResult(_currentUser.User!.Map(_jwtTokenGenerator))
            );
    }

    public async Task<UserResponse> CreateAdmin(NewUserRequest newUserRequest)
    {
        var context = new ValidationContext<NewUserRequest>(newUserRequest);
        var validationResult = await _newUserValidator.ValidateAsync(context);
        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }
        var user = await _newsStore.CreateNewUser(newUserRequest);

        return new UserResponse(user.Map(_jwtTokenGenerator));
    }

    public async Task<SingleArticleResponse> CreateArticle(NewArticleRequest request)
    {
        var context = new ValidationContext<NewArticleRequest>(request);
        var validationResult = await _newArticleValidator.ValidateAsync(context);
        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }
        var article = new Article
        {
            Title = request.Article.Title,
            Description = request.Article.Description,
            Body = request.Article.Body,
            TopicId = request.Article.TopicId,
            Author = _currentUser.User!,
            Slug = _slugifier.Generate(request.Article.Title)
        };
        if (request.Article.TagList.Any())
        {
            var existingTags = await _newsStore.GetExistingTags(request.Article.TagList);

            article.AddTags(existingTags, request.Article.TagList.ToArray());
        }
        await _newsStore.CreateNewArticle(article);

        return new SingleArticleResponse(article.Map());
    }

    public async Task<TopicsResponse> GetTopics()
    {
        return new TopicsResponse(await _newsStore.GetTopics());
    }

    public async Task<NewTopicRequest> AddTopic(NewTopicRequest request)
    {
        var context = new ValidationContext<NewTopicRequest>(request);
        var validationResult = await _newTopicValidator.ValidateAsync(context);
        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }
        await _newsStore.AddTopic(request.TopicName);

        return request;
    }

    public async Task<RemoveTopicResponse> RemoveTopic(string topicId)
    {
        var topic = await _newsStore.RemoveTopic(Int32.Parse(topicId));
        if (topic == null)
        {
            throw new NotFoundException($"{topicId} is not exists");
        }

        return new RemoveTopicResponse(topic.TopicId, topic.TopicName);
    }

    public async Task<SingleArticleResponse> UpdateArticle(UpdateArticleRequest request)
    {
        var context = new ValidationContext<UpdateArticleRequest>(request);
        var validationResult = await _updateArticleValidator.ValidateAsync(context);
        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }

        var article = await _newsStore.FindArticleBySlug(request.Slug, true);

        if (article.UserId != _currentUser.User!.Id)
        {
            throw new ForbiddenException();
        }

        article.Title = request.Article.Title ?? article.Title;
        article.Description = request.Article.Description ?? article.Description;
        article.Body = request.Article.Body ?? article.Body;

        await _newsStore.UpdateNewArticle(article);

        return new SingleArticleResponse(article.Map());
    }

    public async Task RemoveArticle(DeleteArticleRequest request)
    {
        var article = await _newsStore.FindArticleBySlug(request.Slug, false);
        if (article.UserId != _currentUser.User!.Id)
        {
            throw new ForbiddenException();
        }

        await _newsStore.RemoveCommentsByArticleId(article.ArticleId);
        await _newsStore.RemoveArticle(article);
    }

    public async Task<NewTopicRequest> UpdateTopic(int topicId, NewTopicRequest request)
    {
        var context = new ValidationContext<NewTopicRequest>(request);
        var validationResult = await _newTopicValidator.ValidateAsync(context);
        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }

        var topic = await _newsStore.FindTopicById(topicId);

        if (topic != null && topic.TopicId != topicId)
        {
            throw new NotFoundException();
        }
        topic.TopicName = request.TopicName ?? topic.TopicName;
        await _newsStore.UpdateTopic(topic);

        return request;
    }

}
