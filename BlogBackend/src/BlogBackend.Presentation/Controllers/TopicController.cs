namespace BlogBackend.Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using BlogBackend.Infrastructure.Topic.Queries;
using BlogBackend.Infrastructure.UserTopic.Commands;
using Microsoft.AspNetCore.Authorization;
using BlogBackend.Infrastructure.UserTopic.Queries;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TopicController : Controller
{
    private readonly ISender sender;

    public TopicController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> ChooseTags()
    {
        try
        {
            var getAllQuery = new GetAllTopicsQuery();
            var allTopics = await sender.Send(getAllQuery);

            return Ok(allTopics);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[action]/{userId}")]
    public async Task<IActionResult> CreatePreferences([FromBody]IEnumerable<int> topicsIds, int userId)
    {
        try
        {  
            var createListCommand = new CreateUserTopicsListCommand()
            {
                TopicsIds = topicsIds,
                UserId = userId
            };

            await sender.Send(createListCommand);
            
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

        [HttpGet("[action]")]
    public async Task<IActionResult> GetPreferableTopics(int userId)
    {
        try
        {
            var getAllTopicsByUserIdQuery = new GetAllTopicsByUserIdQuery()
            {
                UserId = userId,
            };

            var preferableTopics = await sender.Send(getAllTopicsByUserIdQuery);

            return Ok(preferableTopics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
