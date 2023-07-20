using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HD.Station.QuanLyTinTuc.Mvc.ApiController;

[ApiController]
[Route("api/[controller]")]

public class TopicController : ControllerBase
{
    private readonly INewsService _newService;

    public TopicController(INewsService newsService)
    {
      _newService = newsService;
    }

    [HttpGet(Name = "GetTopics")]
    public async Task<ActionResult<TopicsResponse>> GetTopics()
    {
      var topics = await _newService.GetTopics();

      return topics;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost(Name = "AddTopics")]
    public async Task<ActionResult<NewTopicRequest>> AddTopic([FromBody] NewTopicRequest request)
    {
        var topic = await _newService.AddTopic(request);

        return topic;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{topicId}", Name = "UpdateTopics")]
    public async Task<ActionResult<NewTopicRequest>> UpdateTopic(string topicId, [FromBody] NewTopicRequest request)
    {
        var topic = await _newService.UpdateTopic(Int32.Parse(topicId), request);

        return topic;
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{topicId}", Name = "RemoveTopics")]
    public async Task<ActionResult<RemoveTopicResponse>> RemoveTopic(string topicId)
    {
        var topic = await _newService.RemoveTopic(topicId);

        return topic;
    }
}
