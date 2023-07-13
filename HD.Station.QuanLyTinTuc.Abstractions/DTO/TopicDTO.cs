using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;

using FluentValidation;

namespace HD.Station.QuanLyTinTuc.Abstractions.DTO;

public record TopicsResponse(IEnumerable<Topic> Topics);

public record NewTopicRequest(string TopicName);

public record RemoveTopicResponse(int TopicId, string TopicName);

public class TopicCreateValidator : AbstractValidator<NewTopicRequest>
{
    public TopicCreateValidator(INewsStore newsStore)
    {
        RuleFor(x => x.TopicName).NotNull().NotEmpty();

        RuleFor(x => x.TopicName).MustAsync(
            async (topicName, cancellationToken) => 
            !await newsStore.isTopicNameExists(topicName)
        )
            .WithMessage("This Topic name is already used");
    }
}
