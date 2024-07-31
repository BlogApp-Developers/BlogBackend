namespace BlogBackend.Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using BlogBackend.Infrastructure.Topic.Queries;
using BlogBackend.Core.Topic.Models;
using System.Text.Json;
using BlogBackend.Infrastructure.UserTopic.Commands;
using BlogBackend.Infrastructure.UserTopic.Queries;

[ApiController]
public class TopicController : Controller
{
    private readonly ISender sender;

    public TopicController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet("api/[controller]")]
    public async Task<IActionResult> ChooseTags()
    {
        try
        {
            var getAllQuery = new GetAllQuery();
            var allTopics = await sender.Send(getAllQuery);

            return View(allTopics);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[controller]/[action]/{userId}")]
    public async Task<IActionResult> CreatePreferences([FromBody]IEnumerable<int> topicsIds, int userId)
    {
        try
        {  
            var createListCommand = new CreateListCommand()
            {
                TopicsIds = topicsIds,
                UserId = userId
            };

            await sender.Send(createListCommand);
            
            return RedirectToAction("Index", "Blog", new { userId });
        }
        catch (Exception)
        {
            return RedirectToAction(controllerName: "Topic", actionName: "ChooseTags");
        }
    }
}
